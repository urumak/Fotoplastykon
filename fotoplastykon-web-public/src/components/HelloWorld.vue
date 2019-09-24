<template>
  <v-container class="flex flex-center">
    <v-row>
      <div class="col-8">
        <div class="row">
          <v-card class="my-card col-8">
            <div class="card-title">
              jakiś tytuł
            </div>
            <div class="card-title-line"></div>
            <div class="news-card-content">
              <v-text-field v-model="username"></v-text-field>
              <v-text-field v-model="password"></v-text-field>
              <v-btn v-on:click="login()"></v-btn>
            </div>
          </v-card>
          <v-img class="news-img" src="@/assets/TheGodfather_02.jpg" contain>
          </v-img>
        </div>
        <div class="row">
          <v-img
            class="news-img-reflection"
            src="@/assets/once-upon-a-time-in-hollywood.jpg"
            contain
          >
          </v-img>
          <v-card class="my-card col-8">
            <div class="card-title">
              Lorem ipsum dolor sit ame
            </div>
            <div class="card-title-line"></div>
            <div class="news-card-content-reflection">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam
              dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu
              commodo molestie. Integer non mollis est, vel interdum eros.
              Vestibulum ante ipsum primis in faucibus orci luctus et ultrices
              posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam
              eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien
              semper fermentum.
            </div>
          </v-card>
        </div>
        <div class="row">
          <v-card class="my-card col-8">
            <div class="card-title">
              tytuł tytuł tytuł tytuł tytuł
            </div>
            <div class="card-title-line"></div>
            <div class="news-card-content">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam
              dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu
              commodo molestie. Integer non mollis est, vel interdum eros.
              Vestibulum ante ipsum primis in faucibus orci luctus et ultrices
              posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam
              eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien
              semper fermentum.
            </div>
          </v-card>
          <v-img class="news-img" src="@/assets/intherain.jpg" contain> </v-img>
        </div>
        <div class="row">
          <v-img class="news-img-reflection" src="@/assets/looney.jpg" contain>
          </v-img>
          <v-card class="my-card col-8">
            <div class="card-title">
              tytuł tytuł tytuł tytuł tytuł
            </div>
            <div class="card-title-line"></div>
            <div class="news-card-content-reflection">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam
              dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu
              commodo molestie. Integer non mollis est, vel interdum eros.
              Vestibulum ante ipsum primis in faucibus orci luctus et ultrices
              posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam
              eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien
              semper fermentum.
            </div>
          </v-card>
        </div>
        <div class="row">
          <v-card class="my-card col-8">
            <div class="card-title">
              tytuł tytuł tytuł tytuł tytuł
            </div>
            <div class="card-title-line"></div>
            <div class="news-card-content">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam
              dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu
              commodo molestie. Integer non mollis est, vel interdum eros.
              Vestibulum ante ipsum primis in faucibus orci luctus et ultrices
              posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam
              eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien
              semper fermentum.
            </div>
          </v-card>
          <v-img class="news-img" src="@/assets/bird.jpg" contain> </v-img>
        </div>
        <div class="row">
          <v-img
            class="news-img-reflection"
            src="@/assets/TheGodfather_02.jpg"
            contain
          >
          </v-img>
          <v-card class="my-card col-8">
            <div class="card-title">
              tytuł tytuł tytuł tytuł tytuł
            </div>
            <div class="card-title-line"></div>
            <div class="news-card-content-reflection">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam
              dapibus consectetur nisi, eu facilisis diam. Ut a est at arcu
              commodo molestie. Integer non mollis est, vel interdum eros.
              Vestibulum ante ipsum primis in faucibus orci luctus et ultrices
              posuere cubilia Curae; Pellentesque posuere id est id cursus. Nam
              eu leo eu leo iaculis suscipit. Mauris fermentum ipsum sed sapien
              semper fermentum.
            </div>
          </v-card>
        </div>
      </div>
      <v-col> </v-col>
      <div class="float-right col-3 right-slider">
        <v-card class="vertical-slider"> </v-card>
      </div>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import UsersService from "@/services/UsersService.ts";
import AuthService from "@/services/AuthService.ts";

@Component({})
export default class HelloWorldComponent extends Vue {
  private username: string = "";
  private password: string = "";

  async created() {
    this.loadData();
  }

  async loadData(): Promise<boolean> {

    return true;
  }

  async login(): Promise<void> {
    let pass = this.password;
    let email = this.username;
    (await (this as any).$auth.login)({
      headers: {
        'Content-Type': 'application/json'
      },
      data: {
        userName: email,
        password: pass
      },
      success: function (data: any) {

      },
      error: function (err:any) {
        console.log(err)
      }
    });
    console.log((await (this as any).$auth.user()));
  }
}
</script>
