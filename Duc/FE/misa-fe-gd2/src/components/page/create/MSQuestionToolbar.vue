<template>
    <div class="list-btn" :class="{ 'list-btn-column': mode == 'vertical' }">
        <MSButtonQuestion :title="mode == 'horizontal' ? 'Thư viện học tập' : ''" v-if="mode == 'horizontal'"
            :src="urlLibrary" :dimention="this.dimention" />
        <img src="../../../assets/img/line.svg" v-if="mode == 'horizontal'" />
        <MSButtonQuestion @click="onClickNewQuesion('select')" :title="mode == 'horizontal' ? 'Chọn đáp án' : ''"
            v-tooltip:left="mode == 'vertical' ? 'Thêm câu chọn đáp án' : ''" :dimention="this.dimention"
            :src="urlSelect" />
        <MSButtonQuestion @click="onClickNewQuesion('truefalse')" :title="mode == 'horizontal' ? 'Đúng sai' : ''"
            v-tooltip:left="mode == 'vertical' ? 'Thêm câu đúng sai' : ''" :dimention="this.dimention"
            :src="urlTrueFalse" />
        <MSButtonQuestion @click="onClickNewQuesion('fill')" :title="mode == 'horizontal' ? 'Điền vào chỗ trống' : ''"
            v-tooltip:left="mode == 'vertical' ? 'Thêm câu điền chỗ trống' : ''" :dimention="this.dimention"
            :src="urlFill" />
        <MSButtonQuestion @click="onClickNewQuesion('match')" :title="mode == 'horizontal' ? 'Ghép nối' : ''"
            v-tooltip:left="mode == 'vertical' ? 'Thêm câu ghép nối' : ''" :dimention="this.dimention"
            :src="urlMatch" />
        <MSButtonQuestion @click="onClickNewQuesion('group')" :title="mode == 'horizontal' ? 'Câu hỏi nhóm' : ''"
            v-tooltip:left="mode == 'vertical' ? 'Thêm câu hỏi nhóm' : ''" :dimention="this.dimention"
            :src="urlGroup" />
        <MSButtonQuestion @click="onClickNewQuesion('essay')" :title="mode == 'horizontal' ? 'Tự luận' : ''"
            v-tooltip:left="mode == 'vertical' ? 'Thêm câu tự luận' : ''" :dimention="this.dimention"
            :src="urlEssay" />
        <div class="line" v-if="mode == 'vertical'"></div>
        <MSButtonQuestion @click="onClickNewQuesion('essay')" v-if="mode == 'vertical'" dimention="small"
            v-tooltip:left="'Sắp xếp thứ tự câu hỏi'" :src="urlArrange">
        </MSButtonQuestion>
    </div>
</template>

<script>
import MSButtonQuestion from '../../button/MSButtonQuestion.vue'
export default {
    name: "MSQuestionToolbar",
    props: [
        'mode'
    ],
    components: {
        MSButtonQuestion
    },
    data() {
        return {
            urlSelect: 'https://sisapapp.misacdn.net/lms/img/select.c15dfe74.svg',
            urlTrueFalse: 'https://sisapapp.misacdn.net/lms/img/yesorno.7f4f0b5a.svg',
            urlLibrary: 'https://sisapapp.misacdn.net/lms/img/library.fc851823.svg',
            urlFill: 'https://sisapapp.misacdn.net/lms/img/fill.af676902.svg',
            urlMatch: 'https://sisapapp.misacdn.net/lms/img/pairing.4e1fb31f.svg',
            urlGroup: 'https://sisapapp.misacdn.net/lms/img/group.ca2a19ef.svg',
            urlEssay: 'https://sisapapp.misacdn.net/lms/img/essay.e07e1e68.svg',
            urlArrange: 'https://sisapapp.misacdn.net/lms/img/sort.220d1765.svg'
        }
    },
    computed: {
        dimention() {
            return this.mode === 'vertical' ? 'small' : '';
        }
    },
    methods: {
        /**
         * click tạo câu hỏi mới -> hiện popup tương ứng
         * CreatedBy: Trịnh Huỳnh Đức (20-5-2023)
         */
        onClickNewQuesion(typeQuestion) {
            this.$emit('onClickNewQuesion', typeQuestion)
        },
    }
}
</script>
<style scoped>
.list-btn {
    display: flex;
    align-items: center;
    gap: 56px;
}

.list-btn>img {
    width: 2px;
    height: 98px;
}

.list-btn-column {
    flex-direction: column;
    gap: 12px;
    position: absolute;
    top: 164px;
    right: 88px;
}

.line {
    width: 40px;
    height: 2px;
    background-color: #b6b9ce;
    border-radius: 4px;
    margin: 0;
}
</style>
