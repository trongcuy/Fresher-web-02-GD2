<template>
        <div class="popup-content">
            <!-- khối header trái -->
            <div class="header-left">
                <div class="num-question">Câu 1</div>
            </div>
            <div class="div-center" style="width: 44px;height: 39px;position: absolute;top: 0px;right: 0px;z-index: 3;"><img
                    src="../../assets/img/ic_help.svg" /></div>
            <ckeditor :editor="editor"
                v-model="dataEditor" :config="editorConfig"></ckeditor>
            <!-- <textarea class="input-question" v-show="typePopupAdd == 'more-explane'"
                placeholder="Nhập lời giải tại đây..."></textarea> -->
        </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { mapMutations } from 'vuex'
export default {
    name: 'MSPopupAddExplane',
    components: {
    },
    props: {
        title: {
            type: String,
            default: ''
        },
    },
    data() {
        return {
            editor: ClassicEditor,
            editorConfig: {
                toolbar: ['bold', 'italic', 'underline', 'uploadImage', 'undo', 'redo', '|', '|'],
                placeholder: "Nhập lời giải tại đây..."
            },
            dataEditor: '',//giá trị của ckeditor
        }
    },
    computed: {
        ...mapGetters(['typePopupAdd']),
    },
    methods: {
        ...mapMutations(['setTypePopupAdd']),
    }
}
</script>

<style scoped>
/* tùy chỉnh ckeditor */
.popup-content .ck.ck-toolbar>.ck-toolbar__items {
    justify-content: end;
    padding-right: 35px;
    background-color: #fff2ab;
}

.popup-content .ck.ck-editor__main>.ck-editor__editable:not(.ck-focused) {
    border: none;
    background-color: #fff2ab;
}

.popup-content .ck.ck-toolbar {
    background: #fff2ab !important;
    border: none;
    padding: 0 var(--ck-spacing-small);
}

.popup-content .ck.ck-editor__main>.ck-editor__editable {
    background: #fff2ab;
    border: none;
    padding: 0px 24px;
    margin-top: -4px;
}

.popup-content .ck.ck-editor__main>.ck-editor__editable:focus {
    background-color: #fff2ab;
    outline: none;
    border: none;
    padding: 0px 24px;
    margin-top: -4px;
    box-shadow: 0 0px 0px rgba(90, 125, 141, .16);
}

.popup-content .ck.ck-editor__main>.ck-editor__editable.ck-focused {
    outline: none;
    border: none;
}
.popup-content {
    background-color: #fff2ab;
    width: 100%;
    min-height: 48%;
    height: auto;
    box-sizing: border-box;
    position: relative;
}
.popup-content .header-left {
    position: absolute;
    top: 0px;
    left: 0px;
    z-index: 3;
}
.num-question {
    position: absolute;
    top: 0;
    left: 0;
    padding: 8px 12px;
    width: max-content;
    height: 40px;
    text-align: center;
    border-radius: 10px 0 10px 0;
    font-weight: 700;
    font-size: 16px;
    color: #606266;
    line-height: 24px;
    background-color: #f8e373;
}
</style>
