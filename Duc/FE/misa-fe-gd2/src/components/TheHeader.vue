<template>
    <div class="header">
        <!-- phần menu bên trái -->
        <div class="div-nav">
            <MSMenuItem path="/overview" title="Tổng quan"
                urlIcon="https://sisapapp.misacdn.net/lms/img/ic_chart.870683b6.svg">
            </MSMenuItem>
            <MSMenuItem path="/class" :imgbiger="true" title="Lớp học"
                urlIcon="https://sisapapp.misacdn.net/lms/img/ic_home.47a0479b.svg">
            </MSMenuItem>
            <MSMenuItem path="/course" title="Học liệu"
                urlIcon="https://sisapapp.misacdn.net/lms/img/ic_multi.7d8b269c.svg">
            </MSMenuItem>
            <MSMenuItem path="/training" title="Đào tạo MISA EMIS"
                urlIcon="https://sisapapp.misacdn.net/lms/img/ic_multi.7d8b269c.svg">
            </MSMenuItem>
        </div>
        <!-- phần menu phải -->
        <div class="header-right">
            <MSCombobox class="combobox" :listSelect="['Năm học 2022-2023', 'Năm học 2021-2022']"
                defaultValue="Năm học 2022-2023" src="https://cegovapp.misacdn.net/cegov/img/ic_drop-down.fa70eead.svg">
            </MSCombobox>
            <div class="div-icon"><img src="https://sisapapp.misacdn.net/lms/img/ic_Material.0dcea9c1.svg" /></div>
            <div class="div-icon" v-tooltip:bottom="'Thiết lập'"><img
                    src="https://sisapapp.misacdn.net/lms/img/ic_Setting_24.425ce083.svg" /></div>
            <div class="div-icon" v-tooltip:bottom="'Trợ giúp'"><img
                    src="https://sisapapp.misacdn.net/lms/img/ic_Help_20.fbed8b0e.svg" /></div>
            <div class="div-icon" v-tooltip:bottom="'Thông báo'"><img
                    src="https://sisapapp.misacdn.net/lms/img/ic_Noti_24.9f8729e8.svg" /></div>
            <div class="div-avatar">
                <p>Xin chào, <b>Đức</b></p>
                <img src="../assets/img/avatar-default.png" />
            </div>
        </div>
    </div>
</template>

<script>
import MSMenuItem from './menu/MSMenuItem.vue'
import MSCombobox from './input/MSCombobox.vue'
export default {
    name: "TheHeader",
    props: [],
    components: {
        MSMenuItem,
        MSCombobox
    },
    data() {
        return {

        }
    }
}
</script>

<style scoped>
/* thanh bar */
.header {
    display: flex;
    position: fixed;
    width: 100%;
    top: 0px;
    box-shadow: 0 3px 20px rgba(90, 125, 141, .16);
    align-items: center;
    justify-content: space-between;
    z-index: 100;
    background-color: #fff;
}

.div-nav {
    height: 64px;
    width: auto;
    display: flex;
    align-items: center;
}

.header-right {
    height: 64px;
    width: auto;
    display: flex;
    align-items: center;

}

.div-icon {
    position: relative;
    width: 40px;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
}

.combobox {
    width: 185px;
    margin-right: 12px;
}

.div-avatar {
    padding-right: 16px;
    display: flex;
    align-items: center;
}

.div-avatar>p {
    padding-left: 8px;
    padding-right: 16px;
    color: #4e5b6a;
}

.div-avatar>img {
    width: 47px;
    height: 47px;
}</style>
