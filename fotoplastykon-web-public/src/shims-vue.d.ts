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
            $alert: {
                close(): void
                success(message: string): void
                info(message: string): void
                warning(message: string): void
                danger(message: string): void
                error(message: string): void
                show(message: string, variant: string, options: object): void
            };
            $log: {
                debug(...args: any[]): void;
                info(...args: any[]): void;
                warn(...args: any[]): void;
                error(...args: any[]): void;
                fatal(...args: any[]): void;
            };
            $i18n: {
                locale(): string,
                set(locale: string): void,
                addfunction(locale: string, translations: object): void,
                shortLocale(): string,
                detectLocale(defaultLocale: string): void,
                localeExists(locale: string): boolean,
                keyExists(key: string): boolean,
                setTranslations(messages: any): void,
                languages(): Record<string, string>,
                options(): any[],
                codes(): string[],
                reactive(item: object, codes? : string[]): void,
                translate(key: string, ...replacements : any[]): string,
                $t(key: string, ...replacements : any[]): string,
                __(key: string, ...replacements : any[]): string
            };
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
            alert: {
                close(): void
                success(message: string): void
                info(message: string): void
                warning(message: string): void
                danger(message: string): void
                error(message: string): void
                show(message: string, variant: string, options: object): void
            };
            $log: {
                debug(...args: any[]): void;
                info(...args: any[]): void;
                warn(...args: any[]): void;
                error(...args: any[]): void;
                fatal(...args: any[]): void;
            };
            i18n: {
                locale(): string,
                set(locale: string): void,
                addfunction(locale: string, translations: object): void,
                shortLocale(): string,
                detectLocale(defaultLocale: string): void,
                localeExists(locale: string): boolean,
                keyExists(key: string): boolean,
                setTranslations(messages: any): void,
                languages(): Record<string, string>,
                options(): any[],
                codes(): string[],
                reactive(item: object, codes? : string[]): void,
                translate(key: string, ...replacements : any[]): string,
                $t(key: string, ...replacements : any[]): string,
                __(key: string, ...replacements : any[]): string
            };
        }
    }
}

declare module "vue-progressbar"
{
    import { PluginFunction } from "vue";

    export const install: PluginFunction<{}>;

    interface ProgressMethods
    {
        start(): void;
        finish(): void;
        fail(): void;
    }

    module "vue/types/vue"
    {
        interface Vue
        {
            $Progress: ProgressMethods;
        }
        interface VueConstructor
        {
            Progress: ProgressMethods;
        }
    }
}

declare module 'form-backend-validation';
declare module '@websanova/vue-auth';
declare module 'lodash/merge'
