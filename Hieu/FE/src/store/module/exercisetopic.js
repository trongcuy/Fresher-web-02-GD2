import axios from "axios";
import { constants } from "@/config/config";
import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";
import { handleShowToast, getValueEnum } from "@/common/common";

const state = {
    listExerciseTopic: [], // danh sách id topic
    exerciseTopicUpload: [], // danh sách id topic gửi lên
}

const mutations = {
    /**
     * Lấy danh sách id topic theo id bài tập
     * @param {} state 
     * @param {*} payload 
     * VMHieu 31/05/2023
     */
    getTopicByID(state, payload) {
        state.listExerciseTopic = payload;
    },

    /**
     * Cập nhật danh sách topic gửi lên
     * @param {*} context 
     * @param {*} data 
     */
    updateExerciseTopic(state, payload) {
        state.exerciseTopicUpload = payload;
    },

    /**
     * Lấy danh sách id topic theo id bài tập
     * @param {} state 
     * @param {*} payload 
     * VMHieu 31/05/2023
     */
    postTopicMultiple(state, payload) {
        const a = 0;
    },

    /**
     * Xóa danh sách topics
     * @param {*} context 
     * VMHieu 15/06/2023
     */
    clearExerciseTopics(state) {
        state.listExerciseTopic = [];
    }
}

const actions = {
    /**
     * Lấy danh sách id topic theo id bài tập
     * @param {} state 
     * @param {*} payload 
     * VMHieu 31/05/2023
     */
    async getTopicByID(context, data) {
        try {
            const res = await axios.get(`${constants.API_URL}/api/${constants.API_VERSION}/exercisetopic/id?ExerciseID=${data}`)
            if (res.data) {
                context.commit('getTopicByID', res.data);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.InvalidDataResponse, Enum.ToastStatus.Fail);
            }
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.LoadFail, Enum.ToastStatus.Fail);
        }
    },
    /**
     * Lấy danh sách id topic theo id bài tập
     * @param {} state 
     * @param {*} payload 
     * VMHieu 31/05/2023
     */
    async postTopicMultiple(context, data) {
        try {
            const res = await axios.post(`${constants.API_URL}/api/${constants.API_VERSION}/exercisetopic/multiple?ExerciseID=${data.ExerciseID}`, data.Topics)
            if (res.data) {
                context.commit('postTopicMultiple', res.data);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.InvalidDataResponse, Enum.ToastStatus.Fail);
            }
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.LoadFail, Enum.ToastStatus.Fail);
        }
    },

    /**
     * Xóa danh sách topics
     * @param {*} context 
     * VMHieu 15/06/2023
     */
    clearExerciseTopics(context) {
        context.commit('clearExerciseTopics');
    }
}

export default {
    state,
    mutations,
    actions
}