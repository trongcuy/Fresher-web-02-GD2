<template>
    <div class="ckeditor-answer" ref="box">
        <div class="custom-ckeditor">
            <ckeditor :editor="editor" ref="editor" v-model="editorData" :config="editorConfig" @click="onEditorReady()"></ckeditor>
        </div>
    </div>
</template>
  
<script>
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import * as Resource from '@/common/resource/Resource';
import { mapState } from 'vuex';
import { nextTick } from 'vue';

export default {
    data() {
      return {
        editor: ClassicEditor,
        editorData: '',
        editorConfig: {
            toolbar: [ 'bold', 'italic' ],
        },
        startupFocus: false,
        showPlaceholder: true
      };
    },
    computed: mapState({
      showFormQuestion: (state) => state.question.showFormQuestion
    }),
    mounted() {
      nextTick(() => {
        if (this.dataEditor) {
          this.editorData = this.dataEditor;
        }
      })
    },
    methods: {
      onEditorReady(editor){
        editor.focus();
      }
    },
    watch: {
        // Xem sự thay đổi của v-model để đẩy lên component cha
        editorData: function() {
            this.$emit("update:modelValue", this.editorData);
        },
        dataEditor: function() {
          this.editorData = this.dataEditor;
        },
        /**
         * Reset lại input khi đóng form
         * CreatedBy VMHieu 25/05/2023
         */
         showFormQuestion: function() {
          if (!this.showFormQuestion) {
            this.editorData = "";
          }
        },

        focusEditor: function() {
          if (this.focusEditor) {
            this.$refs.box.classList.add("focus-ckeditor");

            // 
          } else {
            this.$refs.box.classList.remove("focus-ckeditor");
          }
        }
    },
    props: {
      dataEditor: null,
      focusEditor: null
    }
  };

  
</script>

<style>
.ckeditor-answer{
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: block;
    z-index: 1;
    background-size: cover;
    background: no-repeat 50%;
    background-color: #fff;
    padding: 0 8px;
    padding-bottom: 12px;
    box-sizing: border-box;
    background-color: #ffaec7;
}


.focus-ckeditor{
    z-index: 999;
}

.custom-ckeditor{
    border-radius: 10px;
    border: 3px solid;
    border-color: #f59ad4;
    background-color: #fff;
    overflow-x: hidden;
    overflow-y: hidden;
    scrollbar-width: thin;
    /* transition: .2s; */
    height: 100%;
}

.ckeditor-answer p{
    text-align: center;
}

.answer-main .ck-editor .ck-editor__main .ck-content {
    border: none;
    padding: 0px 24px;
    font-size: 16px;
    background-color: #fff;
    height: 100%;
    color: rgb(78, 91, 106);
    box-sizing: border-box;
    overflow: auto;
}

.answer-main .ck.ck-reset.ck-editor.ck-rounded-corners {
    border: none !important;
  outline: none !important;
  box-shadow: none;
  height: 100%;
}

.answer-main .ck.ck-toolbar{
    border: none;
}

.answer-main .ck.ck-toolbar .ck-toolbar__items{
    justify-content: flex-end ;
}

.answer-main .ck.ck-editor__main>.ck-editor__editable:focus{
  border: none !important;
  outline: none !important;
  box-shadow: none;
}

.answer-main .ck.ck-editor__main>.ck-editor__editable.ck-focused{
  border: none !important;
  outline: none !important;
  box-shadow: none;
}

.answer-main .ck.ck-editor__main:focus-visible{
  border: none !important;
  outline: none !important;
  box-shadow: none;
}

.answer-main .ck.ck-editor__main>.ck-editor__editable:focus-visible{
  border: none !important;
  outline: none !important;
  box-shadow: none;
}

.answer-main .ck.ck-editor__top{
  height: 40px;
}

.answer-main .ck.ck-editor__main{
  height: calc(100% - 40px);
}

.answer-main .ck.ck-toolbar.ck-toolbar_grouping{
  background-color: #fff;
}

.answer-main .ck.ck-button.ck-off:hover{
  background-color: #fff;
  cursor: pointer;
}

.answer-main .ck.ck-button.ck-on{
  background-color: #fff;
  cursor: pointer;
}

.answer-main .ck.ck-button.ck-on:hover{
  background-color: #c4c4c4;
  cursor: pointer;
}
  
.answer-main .ck.ck-icon.ck-icon_inherit-color{
  color: inital;
}

.answer-main .ck-placeholder{
  font-size: 18px;
  font-weight: 700;
}

.not-focus .custom-ckeditor{
  border: none;
  background-color: #ffaec7;
}

.not-focus .ck.ck-editor__top{
  display: none;
}

.not-focus .ck.ck-editor__main{
  padding-top: 40px;
  background-color: #ffaec7;
}

.not-focus .ck-editor .ck-editor__main .ck-content {
  background-color: #ffaec7;
}



</style>