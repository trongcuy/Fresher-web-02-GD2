<template>
    <div>
      <ckeditor :editor="editor" v-model="editorData" :config="editorConfig"></ckeditor>
    </div>
</template>
  
<script>
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import * as Resource from '@/common/resource/Resource';
import { mapState } from 'vuex';
  
export default {
    data() {
      return {
        editor: ClassicEditor,
        editorData: '',
        editorConfig: {
            toolbar: [ 'bold', 'italic', 'undo', 'redo', 'uploadImage' ],
            placeholder: Resource.DataCKEditor.placeholderQuestion,
        }
      };
    },
    computed: mapState({
      showFormQuestion: (state) => state.question.showFormQuestion
    }),
    watch: {
        // Xem sự thay đổi của v-model để đẩy lên component cha
        editorData: function() {
          // const parser = new DOMParser();
          // const htmlDoc = parser.parseFromString(this.editorData, 'text/html');
          // let result =  htmlDoc.body.textContent || '';
            this.$emit("update:modelValue", this.editorData);
        },
        /**
         * Reset lại input khi đóng form
         * CreatedBy VMHieu 25/05/2023
         */
        showFormQuestion: function() {
          if (!this.showFormQuestion) {
            this.editorData = "";
          }
        }
    }
  };

  
</script>

<style>
.form-body__main .ck-editor .ck-editor__main .ck-content {
    border: none;
    padding: 0px 24px;
    font-size: 16px;
    background-color: rgb(255, 242, 171);
    height: 100%;
    color: rgb(78, 91, 106);
    border-bottom: 0px;
    border-bottom-left-radius: 0px !important;
    border-bottom-right-radius: 0px !important;
    min-height: 300px;
    box-sizing: border-box;
}

.form-body__main .ck.ck-toolbar .ck-toolbar__items{
    justify-content: flex-end ;
}

.form-body__main .ck.ck-editor__main>.ck-editor__editable:focus{
  border: none !important;
  outline: none !important;
  box-shadow: none;
}

.form-body__main .ck.ck-editor__main>.ck-editor__editable.ck-focused{
  border: none !important;
  outline: none !important;
  box-shadow: none;
}

.form-body__main .ck.ck-editor__main:focus-visible{
  border: none !important;
  outline: none !important;
  box-shadow: none;
}

.form-body__main .ck.ck-editor__main>.ck-editor__editable:focus-visible{
  border: none !important;
  outline: none !important;
  box-shadow: none;
}


.form-body__main .ck.ck-toolbar.ck-toolbar_grouping{
  background-color: #fff2ab;
}

.form-body__main .ck.ck-button.ck-off:hover{
  background-color: #e6e6e6;
  cursor: pointer;
}

.form-body__main .ck.ck-button.ck-on{
  background-color: #e6e6e6;
  cursor: pointer;
}

.form-body__main .ck.ck-button.ck-on:hover{
  background-color: #c4c4c4;
  cursor: pointer;
}
  
.form-body__main .ck.ck-icon.ck-icon_inherit-color{
  color: inital;
}

</style>