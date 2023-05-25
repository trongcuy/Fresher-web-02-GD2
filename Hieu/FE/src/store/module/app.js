const state = {
    showPopup: false,
    showToast: false,
    toastMsg: "",
    popupMsg: "",
    popupStatus: 0,
    toastStatus: 0,
    hideMainPage: false,
    showOver: false,
}

const mutations = {
    /**
     * Mở popup cảnh báo
     * @param {*} state 
     * @param {*} payload
     * CreatedBy VMHieu 05/04/2023 
     */
    showPopup(state, payload) {
        state.showPopup = payload;
    },
    /**
     * Mở toast thông báo
     * @param {*} state 
     * @param {*} payload
     * CreatedBy VMHieu 05/04/2023 
     */
    showToast(state, payload) {
        state.showToast = payload;
    },
        /**
     * Update nội dung toast
     * @param {*} state 
     * @param {*} payload 
     * CreatedBy VMHieu 05/04/2023 
     */
    updateToastMsg(state, payload) {
        state.toastMsg = payload;
    },
    /**
     * Update trạng thái toast
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 23/05/2023 
     */
    updateToastStatus(state, payload) {
        state.toastStatus = payload;
    },
    /**
     * Update trạng thái popup
     * @param {*} state 
     * @param {*} payload 
     * CreatedBy VMHieu 05/04/2023 
     */
    updatePopupStatus(state, payload) {
        state.popupStatus = payload;
    },
    /**
     * Update msg popup
     * @param {*} state 
     * @param {*} payload 
     * CreatedBy VMHieu 05/04/2023 
     */
    updatePopupMsg(state, payload) {
        state.popupMsg = payload;
    },
    /**
     * Ẩn hiện trang chính
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 20/05/2023
     */
    updateHideMainPage(state, payload) {
        state.hideMainPage = payload;
    },

    /**
     * Ẩn hiện over màn hình
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 21/05/2023
     */
    showOver(state, payload) {
        state.showOver = payload;
    }
}

const actions = {
    /**
     * Mở popup cảnh báo
     * @param {*} state 
     * @param {*} data
     * CreatedBy VMHieu 05/04/2023 
     */
    showPopup(context, data) {
        context.commit('showPopup', data);
    },
    /**
     * Mở toast thông báo
     * @param {*} state 
     * @param {*} data
     * CreatedBy VMHieu 05/04/2023 
     */
    showToast(context, data) {
        context.commit('showToast', data);
    },
    /**
     * Update nội dung toast
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 05/04/2023 
     */
    updateToastMsg(context, data) {
        context.commit('updateToastMsg', data);
    },

    /**
     * Update trạng thái toast
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 23/05/2023 
     */
    updateToastStatus(context, data) {
        context.commit('updateToastStatus', data);
    },
    /**
     * Update trạng thái popup
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 05/04/2023 
     */
    updatePopupStatus(context, data) {
        context.commit('updatePopupStatus', data);
    },

    /**
     * Update msg popup
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 05/04/2023 
     */
    updatePopupMsg(context, data) {
        context.commit('updatePopupMsg', data);
    },
    /**
     * Ẩn hiện trang chính
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 20/05/2023
     */
    updateHideMainPage(context, data) {
        context.commit('updateHideMainPage', data);
    },
}

export default {
    state,
    mutations,
    actions
}