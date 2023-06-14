<template>
    <div class="div-main">
        <img src="../../../assets/img/emis.jpg" />
        <div class="div-input">
            <MSInput defaultValue="Nhập tên môn học để tìm kiếm" type="icon-left" :value="searchValue"
                @keydown.enter="onClickFilter" @setValueInput="setValueInput" />
            <div class="div-search div-center"><img src="../../../assets/img/icon_search.svg" /></div>
            <div v-if="this.searchValue" class="icon-select" @click="onClickRemoveSearch"><img
                    src="../../../assets/img/icons8-x-64.png" /></div>
        </div>

        <div class="div-combobox">
            <MSCombobox class="cb-document" :listSelect="['Đang soạn', 'Đã soạn']" :defaultValue="stateValue"
                :iconRemove="true" @setDefaultValue="setValueState"
                src="https://cegovapp.misacdn.net/cegov/img/ic_drop-down.fa70eead.svg">
            </MSCombobox>
            <MSCombobox class="cb-subject" :listSelect="subjectOptions" :defaultValue="subjectValue" :iconRemove="true"
                @setDefaultValue="setValueSubject" src="https://cegovapp.misacdn.net/cegov/img/ic_drop-down.fa70eead.svg">
            </MSCombobox>
            <MSCombobox class="cb-grade" :listSelect="gradeOptions" :defaultValue="gradeValue" :iconRemove="true"
                @setDefaultValue="setValueGrade" src="https://cegovapp.misacdn.net/cegov/img/ic_drop-down.fa70eead.svg">
            </MSCombobox>
        </div>
        <!-- div chứa các card -->
        <div class="div-card" :class="{'card-hidden-btn': exerciseList.length!=pageSize}">
            <MSCard v-for="(item, index) in exerciseList" :exercise="item"
                @click="onClickCardExercise(item.exerciseID)" 
                @onClickRemove="onClickRemove(item)"
                @onClickOpen="onClickCardExercise(item.exerciseID)"/>
        </div>
        <!-- button xem thêm -->
        <MSButton title="Xem thêm" class="btn-more" @click="onClickMore" v-if="exerciseList.length==pageSize"/>
        <!-- ảnh khi ko có bài tập -->
        <div class="no-content" v-if="exerciseList.length==0">
            <img src="../../../assets/img/not-found.svg"/>
            <p>Không tìm thấy học liệu</p>
        </div>
    </div>
    <MSDialog v-if="showDialog" 
        :firstContent="this.resource.DialogContent.removeExercise.firtContent" 
        :lastContent="this.resource.DialogContent.removeExercise.lastContent"
        :data="this.exerciseSelected.exerciseName"
        @onClickOk="onClickOkRemove" 
        @onClickCancel="onClickCancelRemove"/>
	<!-- <MSOverlay v-if="showOverlay"/> -->
</template>

<script>
import MSDialog from '../../notify/MSDialog.vue'
import MSOverlay from '../../popup/MSOverlay.vue'
import MSInput from '../../input/MSInput.vue'
import MSCombobox from '../../input/MSCombobox.vue'
import MSCard from '../../card/MSCard.vue'
import MSButton from '../../button/MSButton.vue'
import { mapGetters } from 'vuex'
import { mapActions } from 'vuex'
import { mapMutations } from 'vuex'
export default {
    name: "CourseMain",
    props: [],
    components: {
        MSDialog,
        MSOverlay,
        MSInput,
        MSCombobox,
        MSCard,
        MSButton
    },
    data() {
        const resource = window.Resource
        return {
            resource,
            showDialog: false,
            showOverlay: false,
            searchValue: '',//giá trị ô tìm kiếm
            stateValue: 'Tất cả học liệu',//trạng thái trong combobox
            subjectValue: 'Tất cả các môn',//giá trị môn học trong combobox
            gradeValue: 'Tất cả các khối',//giá trị môn học trong combobox
            exerciseSelected: {},//lưu id bài tập đang chọn
        }
    },
    watch: {
        //kiểm tra khi có sự thay đổi thì gọi hàm
        pageSize: function () {
            //nếu đang lọc
            if (this.dataFilter.state != 0 || this.dataFilter.subjectID || this.dataFilter.gradeID || this.dataFilter.search)
                this.filterExercise()
            else
                this.getListExercise()
        },
        stateValue: function () {
            this.onClickFilter()
        },
        subjectValue: function () {
            this.onClickFilter()
        },
        gradeValue: function () {
            this.onClickFilter()
        }
    },
    computed: {
        ...mapGetters([
            'pageSize',
            'exerciseList',
            'dataFilter',
            'subjectOptions',
            'gradeOptions',
            'subjectList',
            'gradeList'
        ])
    },
    methods: {
        ...mapMutations(['setPageSize', 'setDataFilter', 'setExerciseIDSelected']),
        ...mapActions(['getListExercise', 'filterExercise', 'getListQuestion', 'deleteExerciseById']),
        /**
         * hàm gọi khi bắt sự kiện click xem thêm
         * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
         */
        onClickMore() {
            this.setPageSize(this.pageSize + 9)
        },
        /**
         * set giá trị input
         * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
         */
        setValueInput(value) {
            this.searchValue = value
        },
        /**
         * bắt sự kiện tìm kiếm
         * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
         */
        onClickFilter() {
            //lấy id của môn học
            let subjectID = ''
            for (const key in this.subjectList) {
                if (this.subjectList[key].subjectName == this.subjectValue) {
                    subjectID = this.subjectList[key].subjectID
                }
            }
            //lấy id của khối
            let gradeID = ''
            for (const key in this.gradeList) {
                if (this.gradeList[key].gradeName == this.gradeValue) {
                    gradeID = this.gradeList[key].gradeID
                }
            }
            //lấy giá trị state
            let state = this.stateValue == "Đang soạn" ? 1 : this.stateValue == "Đã soạn" ? 2 : 0
            const dataFilter = {
                state: state,
                subjectID: subjectID,
                gradeID: gradeID,
                search: this.searchValue,
            }
            this.setDataFilter(dataFilter)
            //gọi api lọc
            this.filterExercise()
        },
        /**
         * set giá trị của state
         * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
         */
        setValueState(value) {
            this.stateValue = value
        },
        /**
         * set giá trị của grade
         * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
         */
        setValueGrade(value) {
            this.gradeValue = value
        },
        /**
         * set giá trị của subject
         * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
         */
        setValueSubject(value) {
            this.subjectValue = value
        },
        /**
         * click xóa tìm kiếm
         * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
         */
        onClickRemoveSearch() {
            this.searchValue = ''
            //gọi lại data
            this.onClickFilter()
        },
        /**
         * click vào thẻ bài tập
         * CreatedBy: Trịnh Huỳnh Đức (30-5-2023)
         */
        onClickCardExercise(exerciseID) {
            //set id bài tập đang chọn
            this.setExerciseIDSelected(exerciseID)
            //đưa đến trang soạn bài
            this.$router.push({ path: "/course/create", query: { exerciseID: exerciseID } })
        },
        /**
         * bắt sự kiện xóa bài tập
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         * @param {*} exerciseID 
         */
        onClickRemove(item) {
            //hiện dialog xác nhận xóa
            this.showOverlay = true
            this.showDialog = true
            //gán id bài tập đã chọn
            this.exerciseSelected = item
        },
        /**
         * bắt sự kiện đồng ý xóa bài tập
         * CreatedBy: Trịnh Huỳnh Đức (31-5-2023)
         * @param {*} exerciseID 
         */
        onClickOkRemove(){
            this.deleteExerciseById(this.exerciseSelected.exerciseID)
            //đóng dialog
            this.showOverlay = false
            this.showDialog = false
        },
        /**
         * hủy xóa bài tập
         * CreatedBy: Trịnh Huỳnh Đức (2-6-2023)
         */
        onClickCancelRemove(){
            //đóng dialog
            this.showOverlay = false
            this.showDialog = false
        },
    }
}
</script>
<style scoped>
.div-main>img {
    margin-top: 64px;
    width: 100%;
    height: auto;
    border-radius: 10px;
    margin-bottom: 16px;
}

.div-combobox {
    display: flex;
    margin-top: 12px;
}

.cb-document {
    width: 150px;
    margin-right: 16px;
}

.cb-subject {
    width: 172px;
    margin-right: 16px;
}

.cb-grade {
    width: 172px;
}

.div-card {
    margin-top: 20px;
    /* display: flex;
    justify-content: space-between; */
    width: 100%;
    /* flex-wrap: wrap; */
    display: grid;
    grid-gap: 24px;
    grid-template-columns: 1fr 1fr 1fr;
}

.btn-more {
    margin-right: 16px;
    min-width: 113px;
    border: none;
    background-color: #8a6bf6;
    color: #fff;
    margin: auto;
    display: block;
    margin-top: 10px;
    margin-bottom: 20px;
}

.btn-more:hover {
    opacity: .8;
    background-color: #8a6bf6;
}

.div-input {
    position: relative;
}

.div-search {
    position: absolute;
    top: 0px;
    left: 0px;
    height: 40px;
    width: 34px;
}

.div-input img {
    width: 16px;
    height: 40px;
}

.icon-select {
    position: absolute;
    top: 0px;
    right: 4px;
    width: 25px;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.icon-select:hover {
    cursor: pointer;
}

.icon-select img {
    width: 14px;
    height: 14px;
}
.card-hidden-btn {
    margin-bottom: 30px;
}
.no-content {
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-bottom: 22px;
    /* margin-top: -30px; */
}
.no-content img {
    height: 128px;
    width: 117px;
    margin-bottom: 24px;
}
</style>
