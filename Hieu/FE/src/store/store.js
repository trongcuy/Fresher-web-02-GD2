import { createStore } from "vuex";
import app from "./module/app";
import question from "./module/question";
import exercise from "./module/exercise";

export default createStore({
    modules: {
        app,
        question,
        exercise
    }
})