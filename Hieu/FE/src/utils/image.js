import axios from "axios";
import { constants } from "@/config/config";
/**
 * Upload ảnh lên BE
 * @param {*} file 
 * @param {*} id 
 * @returns đường dẫn ảnh
 * VMHieu 12/06/2023
 */
export async function uploadImage(file, id) {
    let url;
    let formData = new FormData()
    formData.append("file", file);

    try {
        const res = await axios.post(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/image?idImage=${id}`, formData, {
            headers: {
                "Content-Type": "multipart/form-data",
            }});
        if (res) {
            url = res.data;
        }
    } catch (e) {
        console.log(e);
    }

    return url;
}