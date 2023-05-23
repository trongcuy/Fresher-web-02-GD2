const state = {
    showFormExercise: false,
}

const mutations = {
    /**
     * Hiển thị form thông tin bài tập theo mẫu đã chọn
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 21/05/2023
     */
    showFormExercise(state, payload) {
        state.showFormExercise = payload;
    }
}

const actions = {
    /**
     * Hiển thị form thông tin bài tập theo mẫu đã chọn
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 21/05/2023
     */
    showFormExercise(context, data) {
        context.commit("showFormExercise", data);
    }
}

export default {
    state,
    mutations,
    actions
}