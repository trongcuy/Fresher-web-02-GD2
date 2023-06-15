import { createStore } from "vuex";
import app from "./module/app";
import question from "./module/question";
import exercise from "./module/exercise";
import exercisetopic from "./module/exercisetopic";
import topic from "./module/topic";
import grade from "./module/grade";
import subject from "./module/subject";
import answer from "./module/answer";

export default createStore({
    modules: {
        app,
        question,
        exercise,
        answer,
        exercisetopic,
        grade,
        subject,
        topic
    }
})