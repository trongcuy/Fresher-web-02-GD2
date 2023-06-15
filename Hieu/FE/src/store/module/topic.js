import axios from "axios";
import { constants } from "@/config/config";
import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";
import { handleShowToast, getValueEnum } from "@/common/common";

const state = {
    listTopic: [], //Danh sách chủ đề theo khối và môn học
}

const mutations = {
    /**
     * Lấy danh sách topic theo id khối và môn
     * @param {} state 
     * @param {*} payload 
     * VMHieu 31/05/2023
     */
    getTopicByGradeSubject(state, payload) {
        state.listTopic = payload;
    }
}

const actions = {
    /**
     * Lấy danh sách topic theo id khối và môn
     * @param {} state 
     * @param {*} payload 
     * VMHieu 31/05/2023
     */
    async getTopicByGradeSubject(context, data) {
        try {
            const res = await axios.get(`${constants.API_URL}/api/${constants.API_VERSION}/topic/gradesubject` + 
                `?gradeID=${data.gradeID}&subjectID=${data.subjectID}`)
            if (res.data) {
                context.commit('getTopicByGradeSubject', res.data);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.InvalidDataResponse, Enum.ToastStatus.Fail);
            }
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.LoadFail, Enum.ToastStatus.Fail);
        }
    }
}

export default {
    state,
    mutations,
    actions
}