<template>
    <v-container class="flex flex-center">
        <v-card>
            <v-row>
                <div class="col-3">
                    <v-img v-if="form.photoUrl" contain :src="form.photoUrl"></v-img>
                    <v-img v-else src="@/assets/subPhoto.png"></v-img>
                </div>
                <div class="col-9">
                    <v-file-input
                            accept="image/png, image/jpeg, image/bmp"
                            placeholder="Wybierz zdjęcie"
                            prepend-icon="mdi-camera"
                            label="Zdjęcie"
                            v-model="newPhoto"
                    ></v-file-input>
                    <v-btn class="primary" @click="changePhoto()">Zmień zdjęcie</v-btn>
                </div>
            </v-row>
            <v-form
                    ref="form"
            >
                <v-text-field v-model="form.name" label="Nazwa"></v-text-field>
                <quiz-question-form v-for="(item, index) in form.questions" :key="index" :model="item" @delete-item="deleteItem(index)"></quiz-question-form>
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

    @Component({components:{
            'quiz-question-form': QuizQuestionForm
        }
    })
    export default class QuizFormComponent extends Vue {
        private newPhoto = [];
        private form = new Form({
            id: 0,
            name: '',
            photoUrl: '',
            questions: []
        });

        private get id() : number {
            return Number(this.$route.params.id || 0);
        }

        async created() {
            if(this.id !== 0) this.form = new Form(await QuizzesService.get(this.id));
        }

        async changePhoto(){
            this.form = new Form(await QuizzesService.changePhoto(this.id, this.newPhoto));
        }

        async update(){
            this.form = new Form(await QuizzesService.update(this.id, this.form));
        }

        deleteItem(index: number) {
            this.form.questions.splice(index, 1);
        }

        addItem() {
            this.form.questions.push({
                id: 0,
                questionText: '',
                isMultichoice: false,
                answers: []
            });
        }
    }
</script>