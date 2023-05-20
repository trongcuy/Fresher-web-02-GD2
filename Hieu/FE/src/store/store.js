import { createStore } from "vuex";
import app from "./module/app";

export default createStore({
    modules: {
        app,
    }
})