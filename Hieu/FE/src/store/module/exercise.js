import axios from "axios";
import { constants } from "@/config/config";
import * as Enum from "@/common/enum/Enum";
import * as Resource from "@/common/resource/Resource";
import { handleShowToast, getValueEnum, handleError } from "@/common/common";

const state = {
    showFormExercise: false, // Show form bài tập
    exercises: [], // Mảng các bài tập,
    exercise: {
        ExerciseID: "",
        ExerciseName: "",
        ExerciseImage: "",
        ExerciseStatus: "",
        GradeID: "",
        GradeName: "",
        SubjectID: "",
        SubjectName: "",
        CountQuestion: "",
        CreatedBy: "",
        Question: [{
            QuestionID: "",
            QuestionContent: "",
            QuestionImage: "",
            QuestionNote: "",
            QuestionSecondaryID: "",
            ExerciseID: "",
            SortOder: "",
            TypeQuestion: "",
            Answer: [{
                AnswerID: "",
                AnswerContent: "",
                AnswerImage: "",
                AnswerStatus: "",
                SortOder: "",
                QuestionID: "",
            }]
        }]
    }, // Đối tượng bài tập lấy theo ID
    exerciseID: null, // ID bản ghi được thêm vào mới nhất
    filterDatas: [],    // Mảng chứa dữ liệu lọc
    keyword: "", // Dữ liệu tìm kiếm
    take: 9, // Số bản ghi cần lấy
    totalRecord: 0, // Tổng số bản ghi lấy được
    showExtend: true, // Ẩn hiện nút xem thêm bản ghi,
    formModeExercise: 0, // Mode của page bài tập,
    refresh: false,     // refresh lại trang
    idDelete: "",   // ID của bản ghi cần xóa
    showImport: false, // show form upload
    resultCheckFile: {      // Kết quả check file
        Total: 0,
        TotalSuccess: 0,
        TotalFail: 0
    },
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
    },

    /**
     * Lấy dữ liệu theo phân trang và tìm kiếm
     * @param {*} context 
     * CreatedBy VMHieu 03/04/2023
     */
    getPaging(state, payload) {
        state.exercises = payload.Data;
        state.exercises.map((exercise) => {
            exercise.ExerciseStatus = getValueEnum(exercise.ExerciseStatus, Resource.PropName.ExerciseStatus);
        })

        state.totalRecord = payload.TotalCount;
        state.showExtend = state.totalRecord > state.take;
    },

    /**
     * Lấy dữ liệu theo ID bài tập
     * @param {*} context 
     * CreatedBy VMHieu 30/05/2023
     */
    getAllByID(state, payload) {
        state.exercise = payload.Exercise[0];
        state.exercise.Question = payload.Question;
        
        for(let i = 0; i < state.exercise.Question.length; i++) {
            state.exercise.Question[i].Answer = [];
            for(let j = 0; j < payload.Answer.length; j++) {
                if (state.exercise.Question[i].QuestionID == payload.Answer[j].QuestionID) {
                    state.exercise.Question[i].Answer.push(payload.Answer[j]);
                }
            }
        }
    },

    /**
     * Thay đổi bản ghi được lấy
     * @param {*} context 
     * @param {*} data 
     * VMHieu 30/05/2023
     */
    updateTake(state, payload) {
        state.take = payload;
        state.showExtend = state.totalRecord > state.take;
    },
    /**
     * Thay đổi giá trị lọc và tìm kiếm
     * @param {*} state 
     * @param {*} payload 
     * VMHieu 30/05/2023
     */
    updateFilter(state, payload) {
        state.take = Enum.TakeExercise.Take;
        state.filterDatas = payload;
    }, 
    /**
     * Thay đổi giá trị tìm kiếm
     * @param {*} state 
     * @param {*} payload 
     * VMHieu 30/05/2023
     */
    updateSearch(state, payload) {
        state.keyword = payload || "";
    },
    /**
     * Cập nhật bài tập ở Client
     * @param {} context 
     * @param {*} data 
     * VMHieu 31/05/2023
     */
    updateExercise(state, payload) {
        state.exercise = payload;
    },

    /**
     * Update formmode
     * @param {*} context 
     * @param {*} data
     * VMHieu 01/06/2023 
     */
    updateFormModeExercise(state, payload) {
        state.formModeExercise = payload;
    },

    /**
     * Thêm mới bài tập
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 01/06/2023
     */
    postExercise(state, payload) {
        state.exerciseID = payload;
        state.refresh = !state.refresh;
    },

    /**
     * Sửa bài tập
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 01/06/2023
     */
    putExercise(state) {
        state.refresh = !state.refresh;
    },

    /**
     * Thêm nhiều đối tượng bài tập, câu hỏi, đáp án, chủ đề 
     * VMHieu 06/06/2023
     */
    postMultipleData(state, payload) {
        state.exerciseID = payload
    },

    
    /**
     * Cập nhật id bản ghi cần xóa
     * @param {} context 
     * @param {*} data 
     * VMHieu 06/01/2023
     */
    deleteExercise(state, payload) {
        state.refresh = !state.refresh;
        state.exercises = state.exercises.filter((ex) => ex.ExerciseID !== payload);
        state.totalRecord--
    },

    /**
     * Thực hiện kiểm tra các bản ghi trong file  
     * @param {*} context 
     * @param {*} data 
     *  CreatedBy VMHieu 11/06/2023
     */
    checkFile(state, payload) {
        state.resultCheckFile = payload;
    },

    /**
     * Cập nhật id bản ghi cần xóa
     * @param {} context 
     * @param {*} data 
     * VMHieu 06/01/2023
     */
    updateIdExerciseDelete(state, payload) {
        state.idDelete = payload;
    },

    /**
     * Mở form upload
     * @param {*} context 
     * @param {*} data 
     */
    showImport(state, payload) {
        state.showImport = payload;
    },

    /**
     * Thực hiện import sau khi check
     * @param {*} context 
     * CreatedBy VMHieu 20/04/2023
     */
    importExcel(state, payload) {
        state.exerciseID = payload;
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
    },

    /**
     * Lấy dữ liệu theo phân trang và tìm kiếm
     * @param {*} context 
     * CreatedBy VMHieu 30/05/2023
     */
    async getPaging(context) {       
        try {
            context.commit("showLoading", true);
            const res = await axios.get(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/` + 
                `paging?keyword=${state.keyword}&grade=${state.filterDatas.GradeID}&subject=${state.filterDatas.SubjectID}&` +
                `status=${state.filterDatas.ExerciseStatus}&skip=${constants.Skip}&take=${state.take}`);
            if (res.data) {
                context.commit("getPaging", res.data);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.InvalidDataResponse, Enum.ToastStatus.Fail);
            }
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.LoadFail, Enum.ToastStatus.Fail);
        } finally {
            context.commit("showLoading", false);
        }
    },

    /**
     * Lấy dữ liệu theo ID bài tập
     * @param {*} context 
     * CreatedBy VMHieu 30/05/2023
     */
    async getAllByID(context, data) {   
        try {
            context.commit("showLoading", true);
            const res = await axios.get(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/${data}/overview`);
            if (res.data) {
                context.commit("getAllByID", res.data);
                context.commit("showLoading", false);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.InvalidDataResponse, Enum.ToastStatus.Fail);
            }
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.LoadFail, Enum.ToastStatus.Fail);
        }
    },

    /**
     * Thêm mới bài tập
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 01/06/2023
     */
    async postExercise(context, data) {
        try {
            const res = await axios.post(`${constants.API_URL}/api/${constants.API_VERSION}/exercise`, data)
            context.commit('postExercise', res.data);
            // Hiện toast thành công
            handleShowToast(context, Resource.ToastSuccess.AddSuccess, Enum.ToastStatus.Success);
        } catch (error) {
            // hiện toast thất bại
            //handleShowToast(context, Resource.ToastFail.AddFail, Enum.ToastStatus.Fail);
            handleError(context, error?.response?.data?.errorMsg, Enum.PopupStatus.ErrorServer);
        }
    },

    /**
     * Thêm nhiều đối tượng bài tập, câu hỏi, đáp án, chủ đề 
     * VMHieu 06/06/2023
     */
    async postMultipleData(context, data) {
        try {
            const res = await axios.post(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/multiple`, data)
            context.commit('postMultipleData', res.data);
            context.commit('showFormQuestion', false);
            // Hiện toast thành công
            handleShowToast(context, Resource.ToastSuccess.AddSuccess, Enum.ToastStatus.Success);
        } catch (error) {
            // hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.AddFail, Enum.ToastStatus.Fail);
            handleError(context, error?.response?.data?.errorMsg, Enum.PopupStatus.ErrorServer);
        }
    },

    /**
     * Thêm nhiều đối tượng bài tập, câu hỏi, đáp án, chủ đề 
     * VMHieu 06/06/2023
     */
    async putMultipleData(context, data) {
        try {
            const res = await axios.put(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/multiple`, data)
            //context.commit('postMultipleData', res.data);
            context.commit('showFormQuestion', false);
            // Hiện toast thành công
            handleShowToast(context, Resource.ToastSuccess.EditSuccess, Enum.ToastStatus.Success);
        } catch (error) {
            // hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.EditFail, Enum.ToastStatus.Fail);
            handleError(context, error?.response?.data?.errorMsg, Enum.PopupStatus.ErrorServer);
        }
    },

    /**
     * Sửa bài tập
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 01/06/2023
     */
    async putExercise(context, data) {
        try {
            const res = await axios.put(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/${data.ExerciseID}`, data)
            context.commit('putExercise', res.data);
            // Hiện toast thành công
            handleShowToast(context, Resource.ToastSuccess.EditSuccess, Enum.ToastStatus.Success);
        } catch (error) {
            // hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.EditFail, Enum.ToastStatus.Fail);
            handleError(context, error?.response?.data?.errorMsg, Enum.PopupStatus.ErrorServer);
        }
    },

    /**
     * Xóa bài tập
     * @param {*} context 
     * @param {*} data 
     * CreatedBy VMHieu 05/04/2023
     */
    async deleteExercise(context, id) {
        try {
            const res = await axios.delete(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/${id}`)

            context.commit('deleteExercise', id);
            // Hiện toast thành công
            handleShowToast(context, Resource.ToastSuccess.DeleteSuccess, Enum.ToastStatus.Success);
        } catch (error) {
            // hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.DeleteFail, Enum.ToastStatus.Fail);
        }
    },

    /**
     * Tải file mẫu
     * @param {*} context
     * VMHieu 07/06/2023 
     */
    downloadFileExample(context) {
        try {
            window.open(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/exampleFile`, 'Download');   
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.DownloadFileFail, Enum.ToastStatus.Fail);
        }
    },

    /**
     * Thực hiện kiểm tra các bản ghi trong file  
     * @param {*} context 
     * @param {*} data 
     *  CreatedBy VMHieu 11/06/2023
     */
    async checkFile(context, data) {
        try {          
            context.commit("showLoading", true);
            const res = await axios.post(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/fileCheck`, data, {
                    headers: {
                    'Content-Type': 'multipart/form-data'
                }
            }
            );
            if (res) {
                context.commit("checkFile", res.data);
                context.commit("showImport", true);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.InvalidDataResponse, Enum.ToastStatus.Fail);
            }
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.CheckFail, Enum.ToastStatus.Fail);
            handleError(context, error.response.data.errorMsg, Enum.PopupStatus.ErrorServer);
        } finally {

            context.commit("showLoading", false);
        }
    },

    /**
     * Tải file excel sau khi check
     * @param {} context 
     * CreatedBy VMHieu 28/04/2023
     */
    downloadFileCheck(context, data) {
        try {
            window.open(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/fileCheckResult?statusCheck=${data}`, 'Download');   
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.DownloadFileFail, Enum.ToastStatus.Fail);
        }
    },

    /**
     * Thực hiện import sau khi check
     * @param {*} context 
     * CreatedBy VMHieu 20/04/2023
     */
    async importExcel(context, data) {
        try {
            context.commit("showLoading", true);
            const res = await axios.post(`${constants.API_URL}/api/${constants.API_VERSION}/exercise/import`, data);
            if (res) {
                context.commit("importExcel", res.data);
                handleShowToast(context, Resource.ToastSuccess.ImportSuccess, Enum.ToastStatus.Success);
            }  else {
                // Hiện toast thất bại
                handleShowToast(context, Resource.ToastFail.InvalidDataResponse, Enum.ToastStatus.Fail);
            }     
        } catch (error) {
            // Hiện toast thất bại
            handleShowToast(context, Resource.ToastFail.ImportFail, Enum.ToastStatus.Fail);
        } finally {
            context.commit("showLoading", false);
        }
    },

    /**
     * Thay đổi bản ghi được lấy
     * @param {*} context 
     * @param {*} data 
     * VMHieu 30/05/2023
     */
    updateTake(context, data) {
        context.commit("updateTake", data);
    },

    /**
     * Thay đổi giá trị lọc 
     * @param {*} state 
     * @param {*} payload 
     * VMHieu 30/05/2023
     */
    updateFilter(context, data) {
        context.commit("updateFilter", data);
    },
    /**
     * Thay đổi giá trị tìm kiếm
     * @param {*} state 
     * @param {*} payload 
     * VMHieu 30/05/2023
     */
    updateSearch(context, data) {
        context.commit("updateSearch", data);
    },
    /**
     * Cập nhật bài tập ở Client
     * @param {} context 
     * @param {*} data 
     * VMHieu 31/05/2023
     */
    updateExercise(context, data) {
        context.commit("updateExercise", data);
    },
    /**
     * Update formmode
     * @param {*} context 
     * @param {*} data
     * VMHieu 01/06/2023 
     */
    updateFormModeExercise(context, data) {
        context.commit("updateFormModeExercise", data);
    },
    /**
     * Cập nhật id bản ghi cần xóa
     * @param {} context 
     * @param {*} data 
     * VMHieu 06/01/2023
     */
    updateIdExerciseDelete(context, data) {
        context.commit("updateIdExerciseDelete", data);
    },

    /**
     * Mở form upload
     * @param {*} context 
     * @param {*} data 
     */
    showImport(context, data) {
        context.commit("showImport", data);
    }
}

export default {
    state,
    mutations,
    actions
}