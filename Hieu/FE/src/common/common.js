import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";

/**
 * Hàm xử lý ngoại lệ nhận về từ server
 * @param {*} context 
 * @param {*} errorMsg 
 * @param {*} code 
 * CreatedBy VMHieu 30/05/2023
 */
export function handleError(context, errorMsg, code) {
    context.commit('updatePopupMsg', errorMsg);
    context.commit('showPopup', true);
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