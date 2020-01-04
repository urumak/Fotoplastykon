<template>
    <v-card class="mt-8 ml-5 mr-5">
        <v-row class="mt-8 ml-5">
            <v-text-field class="col-8" v-model="model.questionText" label="Pytanie" :error-messages="errors['QuestionText']"></v-text-field>
            <v-checkbox v-model="model.isMultichoice" label="Wielokrotnego wyboru" :error-messages="errors['IsMultichoice']"></v-checkbox>
            <v-btn class="secondary mt-4 ml-5" small @click="deleteItem()"><v-icon>mdi-minus</v-icon></v-btn>
        </v-row>
        <div class="red--text" v-for="(error, errorIndex) in errors['Answers']" :key="errorIndex">{{ error }}</div>
        <div v-for="(item, index) in model.answers" :key="index" class="ml-12 mr-5">
            <v-row>
                <v-text-field class="col-8"
                              v-model="item.answerText"
                              :label="'Odpowiedź ' + (index + 1).toString()"
                              :error-messages="answersErrors['Answers[' + index.toString() + '].AnswerText']">
                </v-text-field>
                <v-checkbox v-model="item.isCorrect" label="Poprawna"></v-checkbox>
                <v-btn class="secondary mt-4 ml-5" small @click="deleteAnswer(index)"><v-icon>mdi-minus</v-icon></v-btn>
            </v-row>
        </div>
        <v-btn class="secondary mb-2 ml-12" small @click="addItem()"><v-icon>mdi-plus</v-icon></v-btn>
    </v-card>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { Prop, Watch } from 'vue-property-decorator';
    import {PersonInRoleForm, RoleTypeDictionary, PersonDropDownModel} from "@/interfaces/films";
    import FilmsService from '@/services/FilmsService';
    import {QuestionForm} from "@/interfaces/quizes";

    @Component({})
    export default class QuizQuestionFormComponent extends Vue {
        @Prop({default: {
                id: 0,
                questionText: '',
                isMultichoice: false,
                answers: []
            }
        }) private model!: QuestionForm;

        @Prop({default: {}})private errors!: any;
        @Prop({default: {}})private answersErrors!: any;

        deleteItem() {
            this.$emit('delete-item');
        }

        deleteAnswer(index: number) {
            this.model.answers.splice(index, 1);
        }

        addItem() {
            this.model.answers.push({
                id: 0,
                answerText: '',
                isCorrect: false
            });
        }
    }
</script>
