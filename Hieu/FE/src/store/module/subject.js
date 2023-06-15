import axios from "axios";
import { constants } from "@/config/config";
import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";
import { handleShowToast, getValueEnum } from "@/common/common";

const state = {
    subjects: [], // Mảng đối tượng môn học
}

const mutations = {
    /**
     * Lấy tất cả bản ghi môn học
     * @param {} context 
     * CreatedBy VMHieu 28/05/2023
     */
    getAllSubject(state, payload) {
        state.subjects = payload;
    }
}

const actions = {
    /**
     * Lấy tất cả bản ghi môn học
     * @param {} context 
     * CreatedBy VMHieu 28/05/2023
     */
    async getAllSubject(context) {
        try {
            const res = await axios.get(`${constants.API_URL}/api/${constants.API_VERSION}/subject`)
            if (res.data) {
                context.commit('getAllSubject', res.data);
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