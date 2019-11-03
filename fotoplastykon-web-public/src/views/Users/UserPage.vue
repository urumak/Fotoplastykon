<template>
    <v-container class="flex flex-center">
        <div>user page</div>
    </v-container>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";

    @Component({})
    export default class FilmPersonPageComponent extends Vue {
        async created() {
            this.loadData();
        }

        async loadData(): Promise<boolean> {

            return true;
        }
    }
</script>
