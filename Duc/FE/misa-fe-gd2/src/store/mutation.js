export default {
    /*
    * THD(20-4-23)
    * set name
    */
    setName(state, name) {
        state.name = name
    },
    /*
    * set trạng thái overlay
    * CreatedBy: Trịnh Huỳnh Đức (20-5-2023)
    */
    setShowOverlay(state, value) {
        state.showOverlay = value
    },
    /*
    * set loại popupadd
    * CreatedBy: Trịnh Huỳnh Đức (21-5-2023)
    */
    setTypePopupAdd(state, value) {
        state.typePopupAdd = value
    },
}