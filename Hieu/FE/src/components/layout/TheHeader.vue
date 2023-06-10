<template>
    <div class="header-container flex">
        <div class="header-left flex">
            <router-link to="/dashboard" active-class="active">
                <div class="menu-item flex">
                    <div class="menu-item__icon">
                        <img :src="tqImg" alt="" />
                    </div>
                    <div class="menu-item__text" tabindex="0">{{ Resource.TextWeb.Header.Overview }}</div>
                </div>
            </router-link>
            <router-link to="/classroom" active-class="active">
                <div class="menu-item flex">
                    <div class="menu-item__icon">
                        <img :src="lhImg" alt="" />
                    </div>
                    <div class="menu-item__text" tabindex="0">{{ Resource.TextWeb.Header.Grade }}</div>
                </div>
            </router-link>
            <router-link to="/storage" active-class="active">
                <div class="menu-item flex">
                    <div class="menu-item__icon">
                        <img :src="hlImg" alt="" />
                    </div>
                    <div class="menu-item__text" tabindex="0">{{ Resource.TextWeb.Header.Courseware }}</div>
                </div>
            </router-link>
            <router-link to="/training" active-class="active">
                <div class="menu-item flex">
                    <div class="menu-item__icon">
                        <img :src="hlImg" alt="" />
                    </div>
                    <div class="menu-item__text" tabindex="0">{{ Resource.TextWeb.Header.Traning }}</div>
                </div>
            </router-link>  
        </div>
        <div class="header-right flex">
            <div class="menu-icon">
                <img :src="tcImg" alt="" />
            </div>
            <div class="menu-icon">
                <img :src="tlImg" alt="" />
            </div>
            <div class="menu-icon">
                <img :src="tgImg" alt="" width="24" height="24"/>
            </div>
            <div class="menu-icon">
                <img :src="tbImg" alt="" />
            </div>
            <div class="avatar flex">
                <div class="avatar-context">{{ Resource.TextWeb.Header.Greeting }} <strong>Hiếu</strong></div>
                <div class="avatar-img">
                    <img :src="avatarImg" alt="" />
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import * as Resource from "@/common/resource/Resource";

// Các biến lưu đường dẫn
const tqImg = require("@/assets/img/tongquan.svg");
const lhImg = require("@/assets/img/lophoc.svg");
const hlImg = require("@/assets/img/hoclieu.svg");
const tcImg = require("@/assets/img/tuychon.svg");
const tlImg = require("@/assets/img/thietlap.svg");
const tgImg = require("@/assets/img/trogiup.svg");
const tbImg = require("@/assets/img/thongbao.svg");
const avatarImg = require("@/assets/img/getavatar.png");
</script>

<style lang="scss" scoped>
.header-container {
    height: 64px;
    min-height: 64px;
    box-shadow: 0 3px 20px rgba(90,125,141,.16);
    z-index: 999;
    position: sticky;
    top: 0;
    justify-content: space-between;
    background: #fff;

  .header-left,
  .header-right {
    height: 100%;
    width: auto;
    display: flex;
    align-items: center;
  }

  .header-right {
    padding: 16px;
  }

  .menu-item {
    font-size: 16px;
    color: var(--color-text);
    height: 100%;
    padding: 0 8px 0 12px;
    margin: 0 12px;
    cursor: pointer;
    text-decoration: none;

    img {
      margin-right: 8px;
      filter: invert(32%) sepia(26%) saturate(344%) hue-rotate(171deg) brightness(100%) contrast(91%);
    }
  }

  .combobox-year {
    width: 185px;
    margin-right: 0.75rem;
  }

  .menu-icon {
    height: 40px;
    padding: 0 8px;
    display: flex;
    align-items: center;
    cursor: pointer;
  }

  .avatar-context {
    color: var(--color-text);
    font-size: 16px;
    margin: auto 16px auto 8px;
  }

  .avatar-img img {
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

  .active {
    border-bottom: 2px solid #8a6bf6;
    font-weight: 700;
    color: #8a6bf6;
    height: 100%;

    img {
      filter: invert(69%) sepia(72%) saturate(6261%) hue-rotate(227deg) brightness(99%) contrast(94%);
    }

    .menu-item__text {
      font-weight: 700;
      color: #8a6bf6;
    }
  }
}
</style>