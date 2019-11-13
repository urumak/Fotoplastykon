<template>
    <v-container class="flex flex-center">
        <v-row>
            <div class="col-8">
                <v-card v-if="quizState === 0" class="my-card col-8">
                    <v-btn @click="startQuiz()">Rozpocznij</v-btn>
                </v-card>
                <v-card v-if="quizState === 1" class="my-card col-8">
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
                    <div>{{resultModel.name}}</div>
                    <div>{{resultModel.points + "/" + resultModel.questions.length}}</div>
                    <div v-for="item in resultModel.questions" :key="item.id" >
                        <div>{{item.questionText}}</div>
                        <div v-for="answer in item.answers" :key="'a' + answer.id">
                            <div v-if="answer.isCorrect" style="color: green">{{answer.answerText}}</div>
                            <div v-if="!answer.isCorrect && answer.isSelected" style="color: red">{{answer.answerText}}</div>
                            <div v-if="!answer.isCorrect && !answer.isSelected">{{answer.answerText}}</div>
                        </div>
                    </div>
                    <v-btn :to="{ name: 'quizzes' }">Wróć do listy</v-btn>
                </v-card>
            </div>
            <v-col></v-col>
            <div class="float-right col-3 right-slider">
                <v-card class="vertical-slider"></v-card>
            </div>
        </v-row>
    </v-container>
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
            console.log(this.quiz);
        }

        previousQuestion() {
            if(this.questionIndex !== 0) (this.questionIndex)--;
            this.currentQuestion = this.quiz.questions[this.questionIndex];
            console.log(this.quiz);
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
