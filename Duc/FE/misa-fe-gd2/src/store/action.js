//import axios
import axios from 'axios'
axios.defaults.baseURL = 'https://localhost:44393/api/v1'
export default {
    /**
     * lấy danh sách bài tập theo trang
     * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
     * @param {*} param0 
     */
    getListExercise({ state, commit }) {//commit này để gọi đến mutation
        commit('setShowLoading', true)//hiển thị màn hình loading 
        //gọi api
        axios.get(`/Exercises/Paging?pageSize=${state.pageSize}&pageIndex=${state.pageIndex}`)
            .then(response => {
                commit('setExerciseList', response.data);//gọi đến mutation để set list bài tập
                console.log(response.data)
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
            .finally(() => {
                commit('setShowLoading', false) // Ẩn màn hình loading
            })
    },
    /**
     * lấy danh sách môn học
     * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
     * @param {*} param0 
     */
    getListSubject({ commit }) {
        //gọi api
        axios.get(`/Subjects`)
            .then(response => {
                commit('setSubjectList', response.data)
                //lấy danh sách tên các môn học để đưa vào combobox
                let subjectArray = []
                for (const key in response.data) {
                    subjectArray.push(response.data[key].subjectName);
                }
                commit('setSubjectOptions', subjectArray)
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
    },
    /**
     * lấy danh sách khối
     * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
     * @param {*} param0 
     */
    getListGrade({ commit }) {
        //gọi api
        axios.get(`/Grades`)
            .then(response => {
                commit('setGradeList', response.data)
                //lấy danh sách tên các khối để đưa vào combobox
                let gradeArray = []
                for (const key in response.data) {
                    gradeArray.push(response.data[key].gradeName);
                }
                commit('setGradeOptions', gradeArray)
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
    },
    /**
     * lấy danh sách bài tập theo bộ lọc và tìm kiếm
     * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
     * @param {*} param0 
     */
    filterExercise({ state, commit }) {//commit này để gọi đến mutation
        commit('setShowLoading', true)//hiển thị màn hình loading 
        //gọi api
        axios.get(`/Exercises/Filter?state=${state.dataFilter.state}&search=${state.dataFilter.search}&subjectID=${state.dataFilter.subjectID}&gradeID=${state.dataFilter.gradeID}&pageSize=${state.pageSize}&pageIndex=${state.pageIndex}`)
            .then(response => {
                commit('setExerciseList', response.data);//gọi đến mutation để set list bài tập
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
            .finally(() => {
                commit('setShowLoading', false)// Ẩn màn hình loading
            })
    },
    /**
     * lấy danh sách câu hỏi theo id bài tập
     * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
     * @param {*} param0 
     */
    getListQuestion({ commit }, exerciseID) {//commit này để gọi đến mutation
        commit('setShowLoading', true)//hiển thị màn hình loading 
        axios.get(`/Exercises/${exerciseID}/Question`)
            .then(response => {
                commit('setQuestionList', response.data);//gọi đến mutation để set list câu hỏi
                commit('setNumQuestion', response.data.length)
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
            .finally(() => {
                commit('setShowLoading', false) // Ẩn màn hình loading
            })
    },
    /**
     * lấy bài tập theo id
     * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
     * @param {*} param0 
     * @param {*} exerciseID 
     * @returns 
     */
    async getExerciseById({ commit }, exerciseID) {
        commit('setShowLoading', true)
        try {
            const res = await axios.get(`/Exercises/${exerciseID}`)
            commit('setExerciseSelected', res.data)
            commit('setShowLoading', false)
            return res.data
        } catch (error) {
            console.error(error)
            handleException(error.response, { commit })
        }
    },
    /**
     * lấy chủ đề theo lớp và khối
     * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
     * @param {*} param0 
     */
    async getTopic({ commit }, data) {
        try {
            const res = await axios.get(`Topics/SubjectGrade?subjectID=${data.subjectID}&gradeID=${data.gradeID}`);
            commit('setTopicList', res.data)
            return res.data
        } catch (error) {
            console.error(error)
            handleException(error.response, { commit })
        }
    },
    /**
     * thêm một bài tập
     * @param {*} param0 
     * @param {*} data 
     */
    async addExercise({ commit, dispatch }, data) {
        try {
            commit('setShowLoading', true)
            data.exerciseImage = data.exerciseImage ? data.exerciseImage : '00000000-0000-0000-0000-000000000000'
            //gọi api thêm bài tập
            const response = await axios.post('/Exercises', JSON.stringify(data), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            commit('setExerciseIDSelected', response.data)
            dispatch('getListExercise')
            return response.data
        }
        catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
        finally {
            commit('setShowLoading', false)
        }
    },
    /**
     * sửa một bài tập theo id
     * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
     * @param {*} param0 
     * @param {*} data 
     */
    editExercise({ commit, dispatch }, data) {
        commit('setShowLoading', true)
        //gọi api sửa bài tập
        axios.put('/Exercises', JSON.stringify(data), {
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                dispatch('getListExercise')
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
            .finally(() => {
                commit('setShowLoading', false) // Ẩn màn hình loading
            })
    },
    /**
     * xóa bài tập theo id
     * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
     * @param {*} param0 
     * @param {*} exerciseID 
     */
    deleteExerciseById({ commit, dispatch }, exerciseID) {
        commit('setShowLoading', true)
        axios.delete(`/Exercises/${exerciseID}`)
            .then(response => {
                dispatch('getListExercise')
                commit('setShowNotify', "success")
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
            .finally(() => {
                commit('setShowLoading', false) // Ẩn màn hình loading
            })
    },
    /**
     * lấy danh sách chủ đề theo id bài tập
     * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
     * @param {*} param0 
     * @param {*} exerciseID 
     * @returns 
     */
    async getTopicExercise({ commit }, exerciseID) {//commit này để gọi đến mutation
        try {
            const res = await axios.get(`/Exercises/${exerciseID}/Topic`)
            commit('setTopicExercise', res.data)
            return res.data
        }
        catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
    },
    /**
     * thêm nhiều chủ đề cho bài tập
     * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
     * @param {*} param0 
     */
    addTopic({ commit, dispatch, state }) {
        commit('setShowLoading', true)
        //lấy các id của chủ đề
        let topicIDs = ""
        for (const key in state.topicExercise) {
            if (topicIDs)
                topicIDs = topicIDs + ","
            topicIDs = topicIDs + state.topicExercise[key].topicID
        }
        //gọi api thêm chủ đề
        axios.post(`/Exercises/${state.exerciseIDSelected}/Topic?topicIDs=${topicIDs}`)
            .then(response => {
                dispatch('getListExercise')
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
            .finally(() => {
                commit('setShowLoading', false) // Ẩn màn hình loading
            })
    },
    /**
     * thêm một câu hỏi theo id bài tập
     * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
     * @param {*} param0 
     * @param {*} data 
     */
    async addQuestion({ commit, dispatch, state }, data) {
        try {
            commit('setShowLoading', true)
            data.questionNumber = data.questionNumber.toString()
            //gọi api thêm câu hỏi
            const response = await axios.post('/Questions', JSON.stringify(data), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            dispatch('getListQuestion', state.exerciseIDSelected)
            commit('setShowLoading', false)
            return response.data

        }
        catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
    },
    /**
     * xóa câu hỏi theo id
     * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
     * @param {*} param0 
     * @param {*} questionID 
     */
    deleteQuestionById({ commit, dispatch, state }, questionID) {
        commit('setShowLoading', true)
        axios.delete(`/Questions/${questionID}`)
            .then(response => {
                dispatch('getListQuestion', state.exerciseIDSelected)
                commit('setShowNotify', "success")
            })
            .catch(error => {
                console.log(error)
                handleException(error.response, { commit })
            })
            .finally(() => {
                commit('setShowLoading', false) // Ẩn màn hình loading
            })
    },
    /**
     * sửa một câu hỏi theo id bài tập
     * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
     * @param {*} param0 
     * @param {*} data 
     */
    async updateQuestion({ commit, dispatch, state }, data) {
        try {
            commit('setShowLoading', true)
            data.questionNumber = data.questionNumber.toString()
            //gọi api thêm câu hỏi
            const response = await axios.put('/Questions', JSON.stringify(data), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            dispatch('getListQuestion', state.exerciseIDSelected)
            commit('setShowLoading', false)
        }
        catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
    },
    /**
     * lấy danh sách đáp án theo id câu hỏi
     * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
     * @param {*} param0 
     * @param {*} questionID 
     * @returns 
     */
    async getAnswers({ commit }, questionID) {//commit này để gọi đến mutation
        try {
            commit('setShowLoading', true)//hiển thị màn hình loading 
            const response = await axios.get(`/Questions/${questionID}/Answer`)
            commit('setShowLoading', false)//ẩn màn hình loading
            return response.data
        }
        catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
    },
    /**
    * thêm đáp án
    * CreatedBy: Trịnh Huỳnh Đức (4-6-2023)
    * @param {*} param0 
    * @param {*} data 
    */
    async addAnswer({ commit, dispatch, state }, data) {
        try {
            commit('setShowLoading', true)
            //gọi api thêm đáp án
            const response = await axios.post(`/Questions/${data.questionID}/Answer`, JSON.stringify(data.answers), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            dispatch('getListQuestion', state.exerciseIDSelected)
            commit('setShowLoading', false)
        }
        catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
    },

    /**
     * thêm một bài tập, câu hỏi, đáp án, chủ đề
     * CreatedBy: Trịnh Huỳnh Đức (8-6-2023)
     * @param {*} param0 
     * @param {*} data 
     */
    async insertAll({ commit, dispatch, state }, data) {
        try {
            commit('setShowLoading', true)
            //gọi api thêm bài tập
            const response = await axios.post('/Exercises/All', JSON.stringify(data), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            commit('setExerciseIDSelected', response.data)
            dispatch('getListQuestion', state.exerciseIDSelected)
            dispatch('getExerciseById', state.exerciseIDSelected)
            commit('setShowLoading', false)
            return response.data
        }
        catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
    },

    /**
     * upload ảnh 
     * CreatedBy: Trịnh Huỳnh Đức (8-6-2023)
     * @param {*} param0 
     * @param {*} formData 
     */
    async uploadImage({ commit }, formData) {
        try {
            const response = await axios.post('/Images', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
            commit('setImageIdExercise', response.data)
            return response.data
        } catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
    },

    /**
     * tải file excel mẫu
     * CreatedBy: Trịnh Huỳnh Đức (10-6-2023)
     * @param {*} param0 
     */
    dowloadExcelSample({ commit }) {
        commit('setShowLoading', true)
        axios.get('/Excels/Sample', { responseType: 'blob' })
            .then(response => {
                const url = window.URL.createObjectURL(new Blob([response.data]))
                const link = document.createElement('a')
                link.href = url
                link.setAttribute('download', 'Mau_nhap_khau_cau_hoi_loai_1.xlsx')
                document.body.appendChild(link)
                link.click()
            })
            .catch(error => {
                console.error(error);
                handleException(error.response, { commit });
            })
            .finally(() => {
                commit('setShowLoading', false) // Ẩn màn hình loading
            })
    },

    /**
     * upload file excel
     * CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
     * @param {*} param0 
     * @param {*} formData 
     */
    async uploadExcel({ commit }, formData) {
        commit('setShowLoading', true)
        try {
            const response = await axios.post('/Excels', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
            commit('setShowLoading', false)
            return response.data
        } catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
    },

    /**
     * tải file excel lỗi
     * CreatedBy: Trịnh Huỳnh Đức (13-6-2023)
     * @param {*} param0 
     */
    dowloadFileInvalid({ commit }) {
        commit('setShowLoading', true)
        axios.get('/Excels/FileInvalid', { responseType: 'blob' })
            .then(response => {
                const url = window.URL.createObjectURL(new Blob([response.data]))
                const link = document.createElement('a')
                link.href = url
                link.setAttribute('download', 'Loi_nhau_khau_cau_hoi_loai_1.xlsx')
                document.body.appendChild(link)
                link.click()
            })
            .catch(error => {
                console.error(error);
                handleException(error.response, { commit });
            })
            .finally(() => {
                commit('setShowLoading', false) // Ẩn màn hình loading
            })
    },
    /**
     * insert bản ghi hợp lệ vào db
     * CreatedBy: Trịnh Huỳnh Đức (14-6-2023)
     * @param {*} param0 
     * @param {*} exercise 
     * @returns 
     */
    async insertFileValid({ commit, dispatch }, exercise) {
        commit('setShowLoading', true)
        try {
            const response = await axios.post('/Excels/FileValid', JSON.stringify(exercise), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            commit('setShowNotify', 'successImport')
            commit('setExerciseIDSelected', response.data)
            dispatch('getListQuestion', response.data)
            return response.data
        }
        catch (error) {
            console.error(error)
            handleException(error.response, { commit })
        }
        commit('setShowLoading', false)
    },
}

/**
 * hàm xử lý các lối trả về từ server
 * CreatedBy: Trịnh Huỳnh Đức (6-6-2023)
 * @param {*} response 
 * @param {*} param1 
 */
export const handleException = (response, { commit }) => {
    switch (response.status) {
        case 500:
            //hiển thị thông báo
            commit('setShowNotify', "exception")
            break
        case 400: {
            //hiển thị thông báo
            commit('setShowNotify', 'error')
            break
        }
        default: {
            //hiển thị thông báo
            commit('setShowNotify', "exception")
            break
        }
    }
}