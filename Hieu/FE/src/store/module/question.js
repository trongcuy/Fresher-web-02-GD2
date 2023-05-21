const state = {
    showListQuestion: true,
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
    }
}

export default {
    state,
    mutations,
    actions
}