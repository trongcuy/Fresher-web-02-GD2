import axios from "axios";
import { constants } from "@/config/config";
import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";
import { handleShowToast, getValueEnum } from "@/common/common";

const state = {

}

const mutations = {
    
}

const actions = {
    /**
     * Thêm nhiều đáp án
     * @param {} context 
     * CreatedBy VMHieu 02/06/2023
     */
    async postAnswerMultiple(context, data) {
        try {
            const res = await axios.post(`${constants.API_URL}/api/${constants.API_VERSION}/answer/multiple`, data)
            if (res.data) {
                //context.commit('postAnswerMultiple', res.data);
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
    }
}

export default {
    state,
    mutations,
    actions
}