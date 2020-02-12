declare module '*.vue'
{
    import Vue from 'vue';
    export default Vue;
}

declare module "*.json"
{
    const value: any;
    export default value;
}

declare module 'global'
{
    import Vue from 'vue';

    module 'vue/types/vue'
    {
        // Instance properties (this.) can be declared on the `Vue` interface
        interface Vue
        {
            $t(key: string, ...replacements : any[]): string,
            __(key: string, ...replacements : any[]): string,
            abort(code: number, message: string): void,
            handleException(ex: any, rules: Record<number, (ex: any) => void>): void,
            validate(): void
        }
        // Global properties (Vue.) can be declared on the `VueConstructor` interface
        interface VueConstructor
        {
            router: any;
        }
    }
}

declare module 'form-backend-validation';
declare module '@websanova/vue-auth';
declare module 'lodash/merge'
declare module 'moment'
