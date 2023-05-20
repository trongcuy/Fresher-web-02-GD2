<template>
    <RouterLink :to="path" class="rout" :class="{ 'active-router': $route.path.includes(path) }">
        <div class="menu__item">
            <img v-if="urlIcon" :src="urlIcon" :class="{ 'img-biger': imgbiger }">
            <p>{{ title }} </p>
        </div>
    </RouterLink>
</template>

<script>
import { RouterLink } from 'vue-router'
// mỗi menu item có title, src của icon, và path để xd router
export default {
    name: "MSMenuItem",
    props: ["title", "urlIcon", "path", "imgbiger"],//cái class để khi icon nó to hơn thì dùng
    components: { RouterLink },
    data() {
        return {

        }
    }
}
</script>

<style scoped>
.menu__item {
    height: 100%;
    display: flex;
    align-items: center;
    padding: 0px 12px 0px 12px;
    margin: 0px 12px 0px 12px;
    box-sizing: border-box;
    justify-content: space-between;
}

.img-biger {
    width: 30px;
    height: 30px;
}
img {
    width: 24px;
    height: 24px;
    margin-right: 8px;
    filter: invert(19%) sepia(289%) saturate(543%) hue-rotate(747deg);
}
p {
    margin: 0px;
    color: #4e5b6a;
    font-size: 16px;
}
.active-router>div{
    filter: invert(39%) sepia(289%) saturate(543%) hue-rotate(575deg);
    border-bottom: 2px solid #8a6bf6;
    font-weight: 700;
}
.rout {
    color: #000000;
    text-decoration: none;
    height: 100%;
}
</style>