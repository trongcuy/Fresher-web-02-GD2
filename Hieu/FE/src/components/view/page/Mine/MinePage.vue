<template>
    <BaseMinePage>
        <template v-slot:content>
            <div class="list-my-post">
                <div class="list-header">
                    <div class="list-search w-full">
                        <BaseInput showIcon="true" inputClass="m-input" placeholder="Nhập tên học liệu để tìm kiếm"></BaseInput>
                    </div>
                    <div class="list-filter flex">
                        <div class="filter-courseware">
                            <BaseCombobox
                                placeholder="Tất cả học liệu"
                                :propValue=Resource.PropName.RewardLevel
                                propText="Data"
                                :data=Resource.DataRewardLevel
                            ></BaseCombobox>
                        </div>
                        <div class="filter-subject">
                            <BaseCombobox
                                placeholder="Tất cả các môn"
                            ></BaseCombobox>
                        </div>
                        <div class="filter-class">
                            <BaseCombobox
                                placeholder="Tất cả các khối"
                            ></BaseCombobox>
                        </div>
                    </div>
                </div>
                <div class="list-body">
                    <BaseExercise></BaseExercise>
                    <BaseExercise></BaseExercise>
                    <BaseExercise></BaseExercise>
                    <BaseExercise></BaseExercise>
                    <BaseExercise></BaseExercise>
                    <BaseExercise></BaseExercise>
                    <BaseExercise></BaseExercise>
                </div>
                <div class="list-footer">
                    <div class="footer-content center">
                        <BaseButton class="ms-button btn-blue btn-active" text="Xem thêm"></BaseButton>
                    </div>
                </div>
            </div>
        </template>
    </BaseMinePage>
</template>

<script setup>
import BaseButton from '@/components/base/button/BaseButton.vue';
import BaseCombobox from '@/components/base/combobox/BaseCombobox.vue';
import BaseExercise from '@/components/base/exercise/BaseExercise.vue';
import BaseInput from '@/components/base/input/BaseInput.vue';
import BaseMinePage from '@/components/base/minepage/BaseMinePage.vue';
import * as Resource from '@/common/resource/Resource';


</script>

<style scoped>
.list-my-post{
    margin: 0.5rem 0;
    width: 100%;
}

.list-filter{
    margin-top: 0.75rem;
}

.list-filter div{
    margin-right: 16px;
}

.list-filter div:last-child{
    margin-right: 0;
}

.list-body{
    margin-top: 1.25rem;
    padding-bottom: 10px;
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-gap: 24px;
}

.filter-courseware{
    width: 150px;
}

.filter-subject, .filter-class{
    width: 174px;
}
</style>