<template>
    <RouterLink :to="path" class="rout" exact-active-class="active-router">
        <div class="menu__item">
            <img :src="urlIcon" :class="class" class="img">
            <p>{{ title }} </p>
        </div>
    </RouterLink>
</template>

<script>
import { RouterLink } from 'vue-router'
// mỗi menu item có title, src của icon, và path để xd router
export default {
    name: "MenuItem",
    props: ["title", "urlIcon", "path", "class"],//cái class để khi icon nó to hơn thì dùng
    components: { RouterLink },
    data() {
        return {

        }
    }
}
</script>

<style scoped>
.menu__item {
    height: 44px;
    display: flex;
    align-items: center;
    padding: 12px 0px 12px 12px;
    box-sizing: border-box;
    justify-content: space-between;
    margin-top: 4px;
    border-radius: 8px;
}
.img-biger {
    width: 30px;
    height: 30px;
}
.img {
    width: 24px;
    height: 24px;
    margin-right: 12px;
}
p {
    margin: 0px;
}

</style>