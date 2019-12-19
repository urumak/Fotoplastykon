import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import Vue from 'vue';
import {HubConnection} from "@aspnet/signalr/src/HubConnection";
import {Message} from "@/interfaces/chat";

Vue.use(x =>
{
    // use a new Vue instance as the interface for Vue components to receive/send SignalR events
    // this way every component can listen to events or send new events using this.$questionHub
    const chatHub = new Vue()
    Vue.prototype.$chatHub = chatHub

    // Provide methods to connect/disconnect from the SignalR hub
    let connection : any = null;
    let startedPromise : any = null;
    let manuallyClosed = false;

    Vue.prototype.startSignalR = (jwtToken : any) => {
        connection = new HubConnectionBuilder()
            .withUrl(
                process.env.VUE_APP_HUBS_URL + '/chat',
                //@ts-ignore
                jwtToken ? { accessTokenFactory: () => jwtToken } : null
            )
            .configureLogging(LogLevel.Information)
            .build()

        connection.on('ChatMessageReceived', (senderId : number, message: Message) => {
            chatHub.$emit('chat-message-received', senderId, message)
        });
        console.log(connection)

        // You need to call connection.start() to establish the connection but the client wont handle reconnecting for you!
        // Docs recommend listening onclose and handling it there.
        // This is the simplest of the strategies
        function start () {
            startedPromise = connection.start()
                .catch((err : any) => {
                    console.error('Failed to connect with hub', err)
                    return new Promise((resolve, reject) => setTimeout(() => start().then(resolve).catch(reject), 5000))
                })
            return startedPromise
        }
        connection.onclose(() => {
            if (!manuallyClosed) start()
        })

        // Start everything
        manuallyClosed = false
        start()
    }

    Vue.prototype.stopSignalR = () => {
        if (!startedPromise) return

        manuallyClosed = true
        return startedPromise
            .then(() => connection.stop())
            .then(() => { startedPromise = null })
    }
});
