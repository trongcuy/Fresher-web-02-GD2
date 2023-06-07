//import axious
import axios from 'axios';
axios.defaults.baseURL = 'https://localhost:44393/api/v1';
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
                commit('setShowLoading', false)//ẩn màn hình loading
            })
            .catch(error => {
                console.log(error)
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
                commit('setShowLoading', false)//ẩn màn hình loading
            })
            .catch(error => {
                console.log(error)
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
                commit('setShowLoading', false)//ẩn màn hình loading
            })
            .catch(error => {
                console.log(error)
            })
    },
    /**
     * lấy bài tập theo id
     * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
     * @param {*} param0 
     */
    async getExerciseById({ commit }, exerciseID) {
        commit('setShowLoading', true)
        try {
            const res = await axios.get(`/Exercises/${exerciseID}`)
            commit('setExerciseSelected', res.data)
            commit('setShowLoading', false)
            return res.data;
        } catch (error) {
            console.error(error);
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
            return res.data;
        } catch (error) {
            console.error(error);
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
            var jsondata = {
                "exerciseName": data.exerciseName,
                "exerciseState": data.exerciseState,
                "subjectID": data.subjectID,
                "gradeID": data.gradeID
            }
            //gọi api thêm bài tập
            const response = await axios.post('/Exercises', JSON.stringify(jsondata), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            commit('setExerciseIDSelected', response.data)
            dispatch('getListExercise')
            commit('setShowLoading', false)
            return response.data
        }
        catch (error) {
            console.log(error)
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
        //gọi api thêm bài tập
        axios.put('/Exercises', JSON.stringify(data), {
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                dispatch('getListExercise')
                commit('setShowLoading', false)
            })
            .catch(error => {
                console.log(error)
            })
    },
    /**
     * xóa bài tập theo id
     * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
     * @param {*} param0 
     */
    deleteExerciseById({ commit, dispatch }, exerciseID) {
        commit('setShowLoading', true)
        axios.delete(`/Exercises/${exerciseID}`)
            .then(response => {
                commit('setShowLoading', false)
                dispatch('getListExercise')
                commit('setShowNotify', "success") 
            })
            .catch(error => {
                console.log(error)
            })
    },
    /**
     * lấy danh sách chủ đề theo id bài tập
     * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
     * @param {*} param0 
     */
    async getTopicExercise({ commit }, exerciseID) {//commit này để gọi đến mutation
        try {
            const res = await axios.get(`/Exercises/${exerciseID}/Topic`)
            commit('setTopicExercise', res.data)
            return res.data
        }
        catch (error) {
            console.log(error)
        }
    },
    /**
     * thêm nhiều chủ đề cho bài tập
     * CreatedBy: Trịnh Huỳnh Đức (1-6-2023)
     * @param {*} param0 
     * @param {*} topicIDs 
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
                commit('setShowLoading', false)
            })
            .catch(error => {
                console.log(error)
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
            var jsondata = {
                "questionContent": data.questionContent,
                "questionType": data.questionType,
                "questionExplane": data.questionExplane,
                "questionNumber": data.questionNumber.toString(),
                "exerciseID": data.exerciseID
            }
            //gọi api thêm câu hỏi
            const response = await axios.post('/Questions', JSON.stringify(jsondata), {
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
        }
    },
    /**
     * xóa câu hỏi theo id
     * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
     * @param {*} param0 
     */
    deleteQuestionById({ commit, dispatch, state }, questionID) {
        commit('setShowLoading', true)
        axios.delete(`/Questions/${questionID}`)
            .then(response => {
                commit('setShowLoading', false)
                dispatch('getListQuestion', state.exerciseIDSelected)
                commit('setShowNotify', "success") 
            })
            .catch(error => {
                console.log(error)
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
            var jsondata = {
                "questionID": data.questionID,
                "questionContent": data.questionContent,
                "questionType": data.questionType,
                "questionExplane": data.questionExplane,
                "questionNumber": data.questionNumber.toString(),
                "exerciseID": data.exerciseID
            }
            //gọi api thêm câu hỏi
            const response = await axios.put('/Questions', JSON.stringify(jsondata), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            dispatch('getListQuestion', state.exerciseIDSelected)
            commit('setShowLoading', false)
        }
        catch (error) {
            console.log(error)
        }
    },
    /**
     * lấy danh sách đáp án theo id câu hỏi
     * CreatedBy: Trịnh Huỳnh Đức (3-6-2023)
     * @param {*} param0 
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
        }
    },

    /**
     * thêm một bài tập, câu hỏi, đáp án, chủ đề
     * @param {*} param0 
     * @param {*} data 
     */
    async insertAll({ commit, dispatch, state }, data) {
        try {
            debugger
            commit('setShowLoading', true)
            //gọi api thêm bài tập
            const response = await axios.post('/Exercises/All', JSON.stringify(data), {
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            commit('setExerciseIDSelected', response.data)
            dispatch('getListQuestion', state.exerciseIDSelected)
            commit('setShowLoading', false)
            return response.data
        }
        catch (error) {
            console.log(error)
            handleException(error.response, { commit })
        }
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
            //commit('addListException', response.data.userMsg)
            //hiển thị thông báo
            commit('setShowNotify', "exception") 
            break;
        case 400: {
            //hiển thị thông báo
            commit('setShowNotify', 'error')                  
            break;
        }
        default:{ 
            //hiển thị thông báo
            commit('setShowNotify', "exception") 
            break;
        }
    }
}