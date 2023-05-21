import { createStore } from "vuex";
import app from "./module/app";
import question from "./module/question";

export default createStore({
    modules: {
        app,
        question
    }
})