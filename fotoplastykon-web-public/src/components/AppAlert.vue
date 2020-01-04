<template>
    <v-alert class="app-alert"
            :value="show"
            :type="type"
             transition="scroll-y-transition"
             @toggle="closeAlert()"
    >{{ message }}</v-alert>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import { Watch } from 'vue-property-decorator';

    @Component({})
    export default class AppAlertComponent extends Vue {
        private get show(): boolean {
            return this.$store.state.alert.show;
        }
        private get type(): string {
            return this.$store.state.alert.type || "success";
        }
        private get message(): string  {
            return this.$store.state.alert.message;
        }

        @Watch('show')
        closeAlert() {
            if(this.show) {
                (() => setTimeout(() => this.$store.state.alert = {
                    show: false,
                    type: '',
                    message: ''
                }, 5000))();
            }
        }
    }
</script>
