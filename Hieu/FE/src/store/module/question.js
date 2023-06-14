import axios from "axios";
import { constants } from "@/config/config";
import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";
import { handleShowToast, getValueEnum } from "@/common/common";

const state = {
    showListQuestion: false,    // Hiện list câu hỏi
    showFormQuestion: 0,        // Hiện form câu hỏi
    questionID: null, // ID câu hỏi vừa được thêm,
    idDelete: null, // ID câu hỏi chuẩn bị xóa,
    formModeQuestion: null, // Mode của form câu hỏi
    dataQuestion: null, // Data của câu hỏi đang được chọn,
    showFormImage: false, // Hiển thị form thêm ảnh
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
    },

    
    /**
     * Thêm câu hỏi
     * @param {} context 
     * CreatedBy VMHieu 02/06/2023
     */
    async postQuestion(state, payload){
        state.questionID = payload;
    },

    /**
     * Update id cần xóa
     * @param {*} context 
     * @param {*} data 
     * VMHieu 04/06/2023
     */
    updateIdQuestionDelete(state, payload) {
        state.idDelete = payload
    },

    /**
     * Update mode của form câu hỏi
     * @param {*} context 
     * @param {*} data 
     */
    updateFormModeQuestion(state, payload) {
        state.formModeQuestion = payload
    },

    /**
     * Update data của form câu hỏi
     * @param {*} context 
     * @param {*} data 
     */
    updateDataQuestion(state, payload) {
        state.dataQuestion = payload;
    },

    /**
     * Hiển thị form thêm ảnh
     * VMHieu 13/06/2023
     */
    showFormImage(state, payload) {
        state.showFormImage = payload;
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
    },

    /**
     * Thêm câu hỏi
     * @param {} context 
     * CreatedBy VMHieu 02/06/2023
     */
    async postQuestion(context, data) {
        try {
            const res = await axios.post(`${constants.API_URL}/api/${constants.API_VERSION}/question`, data)
            if (res.data) {
                context.commit('postQuestion', res.data);
                //Hiện toast thành công
                handleShowToast(context, Resource.ToastSuccess.AddSuccess, Enum.ToastStatus.Success);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.AddFail, Enum.ToastStatus.Fail);
            }
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.AddFail, Enum.ToastStatus.Fail);
        }
    },

    /**
     * Xóa bản ghi câu hỏi
     * @param {*} context 
     * @param {*} data 
     * VMHieu 04/06/2023
     */
    async deleteQuestion(context, data) {
        try {
            const res = await axios.delete(`${constants.API_URL}/api/${constants.API_VERSION}/question/${data}`)
            if (res.data) {
                //context.commit('deleteQuestion', res.data);
                //Hiện toast thành công
                handleShowToast(context, Resource.ToastSuccess.DeleteSuccess, Enum.ToastStatus.Success);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.DeleteFail, Enum.ToastStatus.Fail);
            }
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.DeleteFail, Enum.ToastStatus.Fail);
        }
    },

    /**
     * Update id cần xóa
     * @param {*} context 
     * @param {*} data 
     * VMHieu 04/06/2023
     */
    updateIdQuestionDelete(context, data) {
        context.commit("updateIdQuestionDelete", data);
    },

    /**
     * Update mode của form câu hỏi
     * @param {*} context 
     * @param {*} data 
     */
    updateFormModeQuestion(context, data) {
        context.commit("updateFormModeQuestion", data);
    },

    /**
     * Update data của form câu hỏi
     * @param {*} context 
     * @param {*} data 
     */
    updateDataQuestion(context, data) {
        context.commit("updateDataQuestion", data);
    },

    /**
     * Hiển thị form thêm ảnh
     * VMHieu 13/06/2023
     */
    showFormImage(context, data) {
        context.commit("showFormImage", data);
    }
}

export default {
    state,
    mutations,
    actions
}