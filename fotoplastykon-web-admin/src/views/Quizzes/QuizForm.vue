<template>
    <v-container class="flex flex-center">
        <v-card>
            <v-row>
                <v-avatar height="300" width="230" :tile="true" class="ml-3">
                    <v-img v-if="model.photoUrl" :src="model.photoUrl"></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </v-avatar>
                <div class="col-9">
                    <v-file-input
                            accept="image/png, image/jpeg, image/bmp"
                            placeholder="Wybierz zdjęcie"
                            prepend-icon="mdi-camera"
                            label="Zdjęcie"
                            v-model="newPhoto"
                    ></v-file-input>
                </div>
            </v-row>
            <v-form
                    ref="form"
            >
                <v-text-field v-model="model.name" label="Nazwa" :error-messages="errors['Name']"></v-text-field>
                <div class="red--text" v-for="(error, errorIndex) in errors['Questions']" :key="errorIndex">{{ error }}</div>
                <quiz-question-form v-for="(item, index) in model.questions"
                                    :key="index" :model="item"
                                    @delete-item="deleteItem(index)"
                                    :error-messages="errors['Questions']"
                                    :errors="getItemErrors(index)"
                                    :answersErrors="getItemAnswerErrors(index)">
                </quiz-question-form>
                <v-row><v-btn class="secondary mt-5 mb-5 ml-12" style="left: 900px" small @click="addItem()"><v-icon>mdi-plus</v-icon></v-btn></v-row>
                <div><v-btn class="primary" @click="update()">Zapisz</v-btn></div>
            </v-form>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import Form from 'form-backend-validation';
    import QuizzesService from "../../services/QuizzesService";
    import QuizQuestionForm from "@/components/quizzes/QuizQuestionForm.vue";
    import {QuizForm} from "@/interfaces/quizes";

    @Component({components:{
            'quiz-question-form': QuizQuestionForm
        }
    })
    export default class QuizFormComponent extends Vue {
        private errors:any = {};
        private newPhoto = [];
        private model: QuizForm = {
            id: 0,
            name: '',
            photoUrl: '',
            questions: []
        };

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.model = new Form(await QuizzesService.get(this.id));
        }

        async update() {
            try {
                if(this.id !== 0) {
                    await QuizzesService.update(this.id, this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await QuizzesService.changePhoto(this.id, this.newPhoto)
                }
                else {
                    let id = await QuizzesService.add(this.model);
                    if(this.newPhoto && this.newPhoto.length !== 0) await QuizzesService.changePhoto(id, this.newPhoto)
                }
                this.$store.state.alert = {
                    show: true,
                    type: 'success',
                    message: 'Zmiany zostały zapisane'
                };
                await this.$router.push({name: 'quizzes'});
            } catch(ex) {
                if (ex.code === 400) this.errors = ex.data.errors;
            }
        }

        deleteItem(index: number) {
            this.model.questions.splice(index, 1);
        }

        addItem() {
            this.model.questions.push({
                id: 0,
                questionText: '',
                isMultichoice: false,
                answers: []
            });
        }

        getItemErrors(index: number) : any {
            return {
                QuestionText: this.errors['Questions[' + index.toString() + '].QuestionText'],
                IsMultichoice: this.errors['Questions[' + index.toString() + '].IsMultichoice'],
                Answers: this.errors['Questions[' + index.toString() + '].Answers']
            }
        }

        getItemAnswerErrors(index: number) {
            let answersCount = this.model.questions[index].answers.length;
            let answersErrors = '{';
            for (let i = 0; i < answersCount; i++) {
                let answerTextError = this.errors['Questions[' + index.toString() + '].Answers[' + i + '].AnswerText'];
                if(answerTextError) {
                    answersErrors = answersErrors + '"Answers[' + i + '].AnswerText":' + JSON.stringify(answerTextError);
                    if(i !== answersCount - 1) answersErrors = answersErrors + ',';
                }
            }
            answersErrors = answersErrors + '}';
            return JSON.parse(answersErrors);
        }
    }
</script>
