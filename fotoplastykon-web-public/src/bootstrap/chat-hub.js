import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import Vue from 'vue';

Vue.use(x =>
{
    const questionHub = new Vue()
    Vue.prototype.$questionHub = questionHub

    let connection = null
    let startedPromise = null
    let manuallyClosed = false

    Vue.prototype.startSignalR = (jwtToken) => {
        connection = new HubConnectionBuilder()
            .withUrl(
              process.env.VUE_APP_HUBS_URL + '/chat',
                jwtToken ? { accessTokenFactory: () => jwtToken } : null
            )
            .configureLogging(LogLevel.Information)
            .build()

        connection.on('QuestionAdded', (question) => {
            questionHub.$emit('question-added', question)
        })
        connection.on('QuestionScoreChange', (questionId, score) => {
            questionHub.$emit('score-changed', { questionId, score })
        })
        connection.on('AnswerCountChange', (questionId, answerCount) => {
            questionHub.$emit('answer-count-changed', { questionId, answerCount })
        })
        connection.on('AnswerAdded', answer => {
            questionHub.$emit('answer-added', answer)
        })
        connection.on('ChatMessageReceived', (username, text) => {
            questionHub.$emit('chat-message-received', { username, text })
        })

        function start () {
            startedPromise = connection.start()
                .catch(err => {
                    console.error('Failed to connect with hub', err)
                    return new Promise((resolve, reject) => setTimeout(() => start().then(resolve).catch(reject), 5000))
                })
            return startedPromise
        }
        connection.onclose(() => {
            if (!manuallyClosed) start()
        })

        manuallyClosed = false
        start()
    }
    Vue.prototype.stopSignalR = () => {
        if (!startedPromise) return

        manuallyClosed = true
        return startedPromise
            .then(() => connection.stop())
            .then(() => { startedPromise = null })
    }

    questionHub.questionOpened = (questionId) => {
        if (!startedPromise) return

        return startedPromise
            .then(() => connection.invoke('JoinQuestionGroup', questionId))
            .catch(console.error)
    }
    questionHub.questionClosed = (questionId) => {
        if (!startedPromise) return

        return startedPromise
            .then(() => connection.invoke('LeaveQuestionGroup', questionId))
            .catch(console.error)
    }
    questionHub.sendMessage = (message) => {
        if (!startedPromise) return

        return startedPromise
            .then(() => connection.invoke('SendLiveChatMessage', message))
            .catch(console.error)
    }
});
