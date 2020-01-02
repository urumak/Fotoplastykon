<template>
    <div>
        <v-card v-if="quizState === 0" class="my-card">
            <v-img :src="quiz.photoUrl">
            </v-img>
            <v-row align="center"><v-col cols="12" align="center">{{ quiz.name }}</v-col></v-row>
            <v-row align="center"><v-col cols="12" align="center"><v-btn @click="startQuiz()">Rozpocznij</v-btn></v-col></v-row>
        </v-card>
        <v-card v-if="quizState === 1" class="my-card">
            <div>
                <div>{{currentQuestion.questionText}}</div>
                <span v-if="currentQuestion.isMultichoice">(więcej niż jedna odpowiedź)</span>
                <div v-if="!currentQuestion.isMultichoice">
                    <v-checkbox hide-details v-for="(answer, i) in currentQuestion.answers" :key="answer.id" :label="answer.answerText" v-model="currentQuestion.answers[i].isSelected" @change="updateAnswers(answer.id)"></v-checkbox>
                </div>
                <div v-else>
                    <v-checkbox hide-details v-for="(answer, i) in currentQuestion.answers" :key="answer.id" :label="answer.answerText" v-model="currentQuestion.answers[i].isSelected" @change="updateAnswers(answer.id)"></v-checkbox>
                </div>
                <v-btn v-if="canGoToPreviousQuestion()" @click="previousQuestion()">Poprzednie</v-btn>
                <v-btn v-if="canGoToNextQuestion()" @click="nextQuestion()">Następne</v-btn>
                <v-btn v-if="canSubmitQuiz()" @click="submitQuiz()">Następne</v-btn>
            </div>
        </v-card>
        <v-card v-if="quizState === 2" class="my-card col-8">
            <div class="col-6 float-left">
                <div>{{resultModel.name}}</div>
                <div>{{resultModel.points + "/" + resultModel.questions.length}}</div>
                <div>"Twoje odpoweidzi"</div>
                <div v-for="item in resultModel.questions" :key="item.id" >
                    <div>{{item.questionText}}</div>
                    <div v-for="answer in item.answers" :key="'a' + answer.id">
                        <div v-if="answer.isCorrect && answer.isSelected" style="color: green">{{answer.answerText}}</div>
                        <div v-if="!answer.isCorrect && answer.isSelected" style="color: red">{{answer.answerText}}</div>
                        <div v-if="answer.isCorrect && !answer.isSelected">{{answer.answerText}}</div>
                        <div v-if="!answer.isCorrect && !answer.isSelected">{{answer.answerText}}</div>
                    </div>
                </div>
            </div>
            <div class="col-6 float-right">
                <div class="mt-12">"Poprawne odpoweidzi"</div>
                <div v-for="item in resultModel.questions" :key="item.id" >
                    <div>{{item.questionText}}</div>
                    <div v-for="answer in item.answers" :key="'a' + answer.id">
                        <div v-if="answer.isCorrect" style="color: green">{{answer.answerText}}</div>
                        <div v-else>{{answer.answerText}}</div>
                    </div>
                </div>
            </div>
            <v-btn :to="{ name: 'quizzes' }">Wróć do listy</v-btn>
        </v-card>
    </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import QuizzesService from "@/services/QuizzesService.ts";
    import {Answer, Question, Quiz, QuizResult, QuizState} from '@/interfaces/quizes';
    import QuestionComponent from '@/components/Question.vue';

    @Component({})
    export default class InformationListComponent extends Vue {
        private quizState: QuizState = QuizState.Starting;
        private questionIndex: number = 0;
        private quiz: Quiz = {
            id: 0,
            name: '',
            questions: []
        };

        private currentQuestion: Question = {
            id: 0,
            questionText: '',
            isMultichoice: false,
            answers: []
        }

        private resultModel: QuizResult = {
            id: 0,
            name: '',
            points: 0,
            questions: []
        }

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            await this.loadData(this.id);
        }

        async loadData(id: number) {
            this.quiz = await QuizzesService.get(id);
            this.currentQuestion = this.quiz.questions[this.questionIndex];
        }

        startQuiz() {
            this.quizState = QuizState.AnsweringQuestions;
        }

        nextQuestion() {
            if(this.questionIndex !== this.quiz.questions.length - 1) (this.questionIndex)++;
            this.currentQuestion = this.quiz.questions[this.questionIndex];
        }

        previousQuestion() {
            if(this.questionIndex !== 0) (this.questionIndex)--;
            this.currentQuestion = this.quiz.questions[this.questionIndex];
        }

        async submitQuiz() {
            this.resultModel = await QuizzesService.submit(this.id, this.quiz);
            this.quizState = QuizState.ShowingResults;
        }

        selectOrUnselectAnswer(item: Answer) {
            item.isSelected = !item.isSelected;
        }

        updateAnswers(id: number) {
            if(!this.currentQuestion.isMultichoice) this.currentQuestion.answers.forEach(x => { if(x.id !== id) x.isSelected = false });
        }

        canGoToPreviousQuestion(): boolean {
            return this.questionIndex !== 0;
        }

        canGoToNextQuestion(): boolean {
            let selectedAnswer = this.currentQuestion.answers.find(x => x.isSelected);
            return this.questionIndex !== this.quiz.questions.length - 1 && (selectedAnswer !== undefined || this.currentQuestion.isMultichoice);
        }

        canSubmitQuiz(): boolean {
            let selectedAnswer = this.currentQuestion.answers.find(x => x.isSelected);
            return this.questionIndex === this.quiz.questions.length - 1 && (selectedAnswer !== undefined || this.currentQuestion.isMultichoice);
        }
    }
</script>
