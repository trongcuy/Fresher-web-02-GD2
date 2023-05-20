<template>
	<div class="div-header">		
		<div class="header-left">
            <MSSubMenuItem path="/course/personal" title="Cá nhân" class="menu-item"/>
            <MSSubMenuItem path="/course/school" title="Trường học" class="menu-item"/>
            <MSSubMenuItem path="/course/community" title="Cộng đồng" class="menu-item"/>
        </div>
        <div class="header-right">
            <MSButton title="Chia sẻ" class="header-share"/>
            <MSButton title="Tạo trò chơi" class="header-game"/>
            <MSButton title="Soạn bài" class="header-edit header-game" @click="onClickCreate"/>
        </div>
	</div>
</template>

<script>
import MSButton from '../../button/MSButton.vue'
import MSSubMenuItem from '../../menu/MSSubMenuItem.vue'
export default {
    name: "CourseHeader",
    props: [],
    components: {
        MSButton,
        MSSubMenuItem
    },
    data() {
        return {

		}
    },
    methods: {
        onClickCreate(){
            this.$router.push("/course/create")
        }
    }
}
</script>
<style scoped>
.div-header {
    display: flex;
    justify-content: space-between;
    position: fixed;
    top: 60px;
    width: 1008px;
    z-index: 99;
    height: 70px;
    padding-top: 16px;
    box-sizing: border-box;
    background-color: #ffffff;
}
.header-left {
    display: flex;
}
.menu-item { 

    background-color: inherit;
}
.header-share {
    min-width: 113px;
    margin-right: 16px;
    background-color: #f1f2f7;
    border: 1px solid #b6b9ce;
}
.header-share:hover {
    background-color: #f1f2f7;
    color: #4e5b6a;
    opacity: .8;
}
.header-game {
    margin-right: 16px;
    min-width: 113px;
    border: none;
    background-color: #8a6bf6;
    color: #fff;
}
.header-game:hover {
    opacity: .8;
    background-color: #8a6bf6;
}
.header-edit {
    margin-right: 0px;
}
.selected {
    background-color: #ece7fe;
    color: #8a6bf6;
}
</style>
