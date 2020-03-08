<template>
    <div>
        <v-card v-if="quizState === 0" class="container-item main-card">
            <v-row align="center">
                <v-col cols="12" align="center">
                    <v-avatar height="400" width="700" :tile="true">
                        <v-img :src="quiz.photoUrl">
                        </v-img>
                    </v-avatar>
                </v-col>
            </v-row>
            <v-row align="center"><v-col cols="12" align="center" style="font-size: 20px">{{ quiz.name }}</v-col></v-row>
            <v-row align="center"><v-col cols="12" align="center"><v-btn class="primary" @click="startQuiz()">Rozpocznij</v-btn></v-col></v-row>
        </v-card>
        <v-card v-if="quizState === 1" class="container-item main-card">
            <div>
                <div>{{currentQuestion.questionText}}</div>
                <span v-if="currentQuestion.isMultichoice">(więcej niż jedna odpowiedź)</span>
                <div v-if="!currentQuestion.isMultichoice">
                    <v-checkbox hide-details v-for="(answer, i) in currentQuestion.answers" :key="answer.id" :label="answer.answerText" v-model="currentQuestion.answers[i].isSelected" @change="updateAnswers(answer.id)"></v-checkbox>
                </div>
                <div v-else>
                    <v-checkbox hide-details v-for="(answer, i) in currentQuestion.answers" :key="answer.id" :label="answer.answerText" v-model="currentQuestion.answers[i].isSelected" @change="updateAnswers(answer.id)"></v-checkbox>
                </div>
                <v-row class="mt-5">
                    <v-col cols="6">
                        <v-btn v-if="canGoToPreviousQuestion()" @click="previousQuestion()"><v-icon left>mdi-arrow-left</v-icon> Poprzednie</v-btn>
                    </v-col>
                    <v-col cols="6">
                        <v-btn class="float-right" v-if="canGoToNextQuestion()" @click="nextQuestion()">Następne <v-icon right>mdi-arrow-right</v-icon></v-btn>
                        <v-btn class="float-right" v-if="canSubmitQuiz()" @click="submitQuiz()">Zakończ <v-icon right>mdi-check</v-icon></v-btn>
                    </v-col>
                </v-row>
            </div>
        </v-card>
        <v-card v-if="quizState === 2" class="container-item main-card">
            <v-row align="center"><v-col cols="12" align="center" style="font-size: 24px">{{ resultModel.name }}</v-col></v-row>
            <v-row align="center"><v-col cols="12" align="center" style="color: #9277ce">{{ "Wynik: " + resultModel.points + "/" + resultModel.questions.length}}</v-col></v-row>
            <v-row>
                <div class="col-6 float-left">
                    <div style="font-size: 20px" class="mb-5">Twoje odpowiedzi</div>
                    <v-card class="container-item pa-3" v-for="item in resultModel.questions" :key="item.id" >
                        <div>{{item.questionText}}</div>
                        <div v-for="answer in item.answers" :key="'a' + answer.id">
                            <li v-if="answer.isCorrect && answer.isSelected" style="color: green">{{answer.answerText}}</li>
                            <li v-if="!answer.isCorrect && answer.isSelected" style="color: red">{{answer.answerText}}</li>
                            <li v-if="answer.isCorrect && !answer.isSelected">{{answer.answerText}}</li>
                            <li v-if="!answer.isCorrect && !answer.isSelected">{{answer.answerText}}</li>
                        </div>
                    </v-card>
                </div>
                <div class="col-6 float-right">
                    <div style="font-size: 20px" class="mb-5">Poprawne odpowiedzi</div>
                    <v-card class="container-item pa-3" v-for="item in resultModel.questions" :key="item.id" >
                        <div>{{item.questionText}}</div>
                        <div v-for="answer in item.answers" :key="'a' + answer.id">
                            <li v-if="answer.isCorrect" style="color: green">{{answer.answerText}}</li>
                            <li v-else>{{answer.answerText}}</li>
                        </div>
                    </v-card>
                </div>
            </v-row>

            <v-btn :to="{ name: 'quizzes' }"><v-icon left>mdi-arrow-left</v-icon> Wróć do listy</v-btn>
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
            photoUrl: '',
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

        updateAnswers(id: number) {
            if(!this.currentQuestion.isMultichoice) this.currentQuestion.answers.forEach(x => { if(x.id !== id) x.isSelected = false });
        }

        canGoToPreviousQuestion(): boolean {
            return this.questionIndex !== 0;
        }

        canGoToNextQuestion(): boolean {
            let selectedAnswer = this.currentQuestion.answers.find(x => x.isSelected);
            return this.questionIndex !== this.quiz.questions.length - 1 && selectedAnswer !== undefined;
        }

        canSubmitQuiz(): boolean {
            let selectedAnswer = this.currentQuestion.answers.find(x => x.isSelected);
            return this.questionIndex === this.quiz.questions.length - 1 && selectedAnswer !== undefined;
        }
    }
</script>
