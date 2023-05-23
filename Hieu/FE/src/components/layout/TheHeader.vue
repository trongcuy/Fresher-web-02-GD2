<template>
    <div class="header-container flex">
        <div class="header-left flex">
            <router-link to="/dashboard" active-class="active">
                <div class="menu-item flex">
                    <div class="menu-item__icon">
                        <img src="@/assets/img/tongquan.svg" alt="" />
                    </div>
                    <div class="menu-item__text">Tổng quan</div>
                </div>
            </router-link>
            <router-link to="/classroom" active-class="active">
                <div class="menu-item flex">
                    <div class="menu-item__icon">
                        <img src="@/assets/img/lophoc.svg" alt="" />
                    </div>
                    <div class="menu-item__text">Lớp học</div>
                </div>
            </router-link>
            <router-link to="/storage" active-class="active">
                <div class="menu-item flex">
                    <div class="menu-item__icon">
                        <img src="@/assets/img/hoclieu.svg" alt="" />
                    </div>
                    <div class="menu-item__text">Học liệu</div>
                </div>
            </router-link>
            <router-link to="/training" active-class="active">
                <div class="menu-item flex">
                    <div class="menu-item__icon">
                        <img src="@/assets/img/hoclieu.svg" alt="" />
                    </div>
                    <div class="menu-item__text">Đào tạo MISA EMIS</div>
                </div>
            </router-link>  
        </div>
        <div class="header-right flex">
            <div class="combobox-year">
                <BaseCombobox></BaseCombobox>
            </div>
            <div class="menu-icon">
                <img src="@/assets/img/tuychon.svg" alt="" />
            </div>
            <div class="menu-icon">
                <img src="@/assets/img/thietlap.svg" alt="" />
            </div>
            <div class="menu-icon">
                <img src="@/assets/img/trogiup.svg" alt="" width="24" height="24"/>
            </div>
            <div class="menu-icon">
                <img src="@/assets/img/thongbao.svg" alt="" />
            </div>
            <div class="avatar flex">
                <div class="avatar-context">Xin chào, <strong>Hiếu</strong></div>
                <div class="avatar-img">
                    <img src="@/assets/img/getavatar.png" alt="" />
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseCombobox from '../base/combobox/BaseCombobox.vue';
</script>

<style scoped>
.header-container {
    height: 64px;
    min-height: 64px;
    box-shadow: 0 3px 20px rgba(90,125,141,.16);
    z-index: 999;
    position: sticky;
    top: 0;
    justify-content: space-between;
    background: #fff;
}

.header-left, .header-right {
    height: 100%;
    width: auto;
    display: flex;
    align-items: center;
}

.menu-item {
    font-size: 16px;
    color: var(--color-text);
    height: 100%;
    padding: 0 8px 0 12px;
    margin: 0 12px;
    cursor: pointer;
    text-decoration: none;
}

.menu-item img{
    margin-right: 8px;
    filter: invert(32%) sepia(26%) saturate(344%) hue-rotate(171deg) brightness(100%) contrast(91%);
}

.combobox-year{
    width: 185px;
    margin-right: 0.75rem;
}

.menu-icon{
    height: 40px;
    padding: 0 8px;
    display: flex;
    align-items: center;
    cursor: pointer;
}

.avatar-context{
    color: var(--color-text);
    font-size: 16px;
    margin: auto 16px auto 8px;
}

.avatar-img img{
    height: 47px;
    width: 47px;
    background-size: 100%;
    background-repeat: none;
    background-color: #f1f2f7;
    border: 1px solid #f1f2f7;
    border-radius: 50%;
    cursor: pointer;
    overflow: hidden;
}

.active{
    border-bottom: 2px solid #8a6bf6;
    font-weight: 700;
    color: #8a6bf6;
    height: 100%;
}

.active img{
    filter: invert(69%) sepia(72%) saturate(6261%) hue-rotate(227deg) brightness(99%) contrast(94%);
}

.active .menu-item__text{
    font-weight: 700;
    color: #8a6bf6;
}
</style>