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
    addExercise({ commit, dispatch }, data) {
        commit('setShowLoading', true)
        var jsondata = {
            "exerciseName": data.exerciseName,
            "exerciseState": data.exerciseState,
            "subjectID": data.subjectID,
            "gradeID": data.gradeID
        }
        //gọi api thêm bài tập
        axios.post('/Exercises', JSON.stringify(jsondata), {
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
        for (const key in state.topicList) {
            if (state.topicExercise.includes(state.topicList[key].topicName)) {
                if(!topicIDs)
                    topicIDs = topicIDs + "," 
                    topicIDs = topicIDs + state.topicList[key].topicID
            }  
        }
        console.log(topicIDs)
        //gọi api thêm chủ đề
        // axios.post(`/Exercises/${state.exerciseIDSelected}/Topic?topicIDs=${topicIDs}`)
        //     .then(response => {
        //         dispatch('getListExercise')
        //         commit('setShowLoading', false)
        //     })
        //     .catch(error => {
        //         console.log(error)
        //     })
    },
}