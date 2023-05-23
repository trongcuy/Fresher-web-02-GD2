<template>
    <RouterLink :to="path" class="rout" :class="{ 'active-router': $route.path === path }">
        <div class="menu__item">
            <p>{{ title }} </p>
        </div>
    </RouterLink>
</template>

<script>
import { RouterLink } from 'vue-router'
// mỗi menu item có title, src của icon, và path để xd router
export default {
    name: "MSMenuItem",
    props: ["title", "path"],//cái class để khi icon nó to hơn thì dùng
    components: { RouterLink },
    data() {
        return {

        }
    }
}
</script>

<style scoped>
.menu__item {
    height: 40px;
    display: flex;
    align-items: center;
    box-sizing: border-box;
    border-radius: 10px;
}
p {
    padding: 0px 12px;
    font-weight: 500;
    font-size: 14px;
}
.active-router>div{
    background-color: #ece7fe;
    color: #8a6bf6;
}
.active-router>div>p {
    color: #8a6bf6;
}
.rout {
    color: #000000;
    text-decoration: none;
    height: 100%;
}
</style>