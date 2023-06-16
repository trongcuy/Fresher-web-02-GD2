import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";

/**
 * Hàm xử lý ngoại lệ nhận về từ server
 * @param {*} context 
 * @param {*} errorMsg 
 * @param {*} code 
 * CreatedBy VMHieu 30/05/2023
 */
export function handleError(context, errorMsg, status) {
    context.commit('updatePopupMsg', errorMsg);
    context.commit('showPopup', true);
    context.commit('updatePopupStatus', status);
}

/**
 * Show toast thông báo mỗi khi thực hiện xong thao tác
 * CreatedBy VMHieu 30/05/2023
 */
export function handleShowToast(context, msg, status) {
    context.commit('updateToastStatus', status);
    context.commit('updateToastMsg', msg);
    context.commit('showToast', true);
    setTimeout(() => {
        context.commit('showToast', false);
    }, 2000);
    context.commit("showLoading", false);
}

/**
 * Lấy giá trị của một enum
 * @param {*} data 
 * @param {*} resourceName 
 * @returns 
 * CreatedBy VMHieu 30/05/2023
 */
export function getValueEnum (data, enumName) {
    let enumeration = Enum[enumName],
        resource = Resource[enumName];

    for(const prop in enumeration) {
        if(enumeration[prop] == data){
            data = resource[prop];
        }
    }
    
    return data;
}
/**
 * Thay thế breakpoint trong msg bằng point
 * @param {*} msg 
 * @param {*} point 
 * @returns 
 * VMHieu 01/06/2023
 */
export function changeBreakpoint(msg, point) {
    let arr = msg.trim().split(" ");
    for(let i = 0; i < arr.length; i++) {
        if (arr[i] == Resource.PopupMessage.Breakpoint) {
            arr[i] = '"' + point + '"';
        }
    }
    return arr.join(" ");
}
/**
 * Biến đổi html về text
 * @param {*} html 
 * @returns 
 */
export function convertToText (html) {
    // Tạo một thẻ div tạm thời để chứa dữ liệu HTML
    let tempDiv = document.createElement("div");
    tempDiv.innerHTML = html;

    // Sử dụng thuộc tính textContent để lấy nội dung văn bản thuần túy từ thẻ div
    let plainText = tempDiv.textContent;

    return plainText;
}
/**
 * Tạo một UUID
 * @returns 
 * VMHieu 12/06/2023
 */
export function generateUUID() { // Public Domain/MIT
    var d = new Date().getTime();//Timestamp
    var d2 = ((typeof performance !== 'undefined') && performance.now && (performance.now()*1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16;//random number between 0 and 16
        if(d > 0){//Use timestamp until depleted
            r = (d + r)%16 | 0;
            d = Math.floor(d/16);
        } else {//Use microseconds since page-load if supported
            r = (d2 + r)%16 | 0;
            d2 = Math.floor(d2/16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}

/**
 * Hiện toast thông báo
 * @param {*} store 
 * @param {*} msg 
 * VMHieu 15/06/2023
 */
export function showToastWarning(store, msg){
    store.dispatch("showToast", true);
    store.dispatch("updateToastStatus", Enum.ToastStatus.Warning);
    store.dispatch("updateToastMsg", msg);
}



