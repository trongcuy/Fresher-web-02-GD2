const state = {
    showPopup: false,   // Show popup
    showToast: false,   // Show toast
    toastMsg: "",       
    popupMsg: "",
    popupStatus: 0,
    toastStatus: 0,
    hideMainPage: false, // Show page chính
    statusDelete: 0,    // Trạng thái xóa
    showLoading: false, // Hiển thị loading ,
    formModeUpload: 0,
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
        setTimeout(() => {
            state.showToast = false;
        }, 3000);
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
    },

    /**
     * Update trạng thái xóa của popup
     * @param {*} context 
     * @param {*} data 
     * VMHieu 04/06/2023
     */
    updateStatusDelete(state, payload) {
        state.statusDelete = payload;
    },

    /**
     * Ẩn hiện loading
     * @param {*} context 
     * @param {*} data
     * VMHieu 05/06/2023 
     */
    showLoading(state, payload) {
        state.showLoading = payload;
        setTimeout(() => {
            state.showLoading = false;
        }, 3000);
    },

    /**
     * Upload mode của form upload ảnh
     * @param {*} context 
     * @param {*} data 
     */
    uploadFormModeUpload(state, payload) {
        state.formModeUpload = payload;
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

    /**
     * Update trạng thái xóa của popup
     * @param {*} context 
     * @param {*} data 
     * VMHieu 04/06/2023
     */
    updateStatusDelete(context, data) {
        context.commit('updateStatusDelete', data);
    },

    /**
     * Ẩn hiện loading
     * @param {*} context 
     * @param {*} data
     * VMHieu 05/06/2023 
     */
    showLoading(context, data) {
        context.commit('showLoading', data);
    },

    /**
     * Upload mode của form upload ảnh
     * @param {*} context 
     * @param {*} data 
     */
    uploadFormModeUpload(context, data) {
        context.commit('uploadFormModeUpload', data);
    }
}

export default {
    state,
    mutations,
    actions
}