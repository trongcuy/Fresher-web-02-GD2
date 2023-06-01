import axios from "axios";
import { constants } from "@/config/config";
import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";
import { handleShowToast, getValueEnum } from "@/common/common";

const state = {
    showListQuestion: false,
    showFormQuestion: false,
}

const mutations = {
    /**
     * Ẩn hiện list câu hỏi
     * @param {*} context 
     * @param {*} data 
     * Created by VMHieu 21/05/2023
     */
    showListQuestion(state, payload) {
        state.showListQuestion = payload;
    },

    /**
     * Hiển thị form tạo câu hỏi theo mẫu đã chọn
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 21/05/2023
     */
    showFormQuestion(state, payload) {
        state.showFormQuestion = payload;
    }
}

const actions = {
    /**
     * Ẩn hiện list câu hỏi
     * @param {*} context 
     * @param {*} data 
     * Created by VMHieu 21/05/2023
     */
    showListQuestion(context, data) {
        context.commit("showListQuestion", data);
    },
    /**
     * Hiển thị form tạo câu hỏi theo mẫu đã chọn
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 21/05/2023
     */
    showFormQuestion(context, data) {
        context.commit("showFormQuestion", data);
    }
}

export default {
    state,
    mutations,
    actions
}