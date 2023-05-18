<template>
    <div class="div-header">
        <MenuItem path="/overview" title="Menu item" urlIcon="https://sisapapp.misacdn.net/lms/img/ic_chart.870683b6.svg"></MenuItem>
        <MenuItem path="/class" class="img-biger" title="Menu item" urlIcon="https://sisapapp.misacdn.net/lms/img/ic_home.47a0479b.svg"></MenuItem>
        <MenuItem path="/document" title="Menu item" urlIcon="https://sisapapp.misacdn.net/lms/img/ic_multi.7d8b269c.svg"></MenuItem> 
        <MenuItem path="/training" title="Menu item" urlIcon="https://sisapapp.misacdn.net/lms/img/ic_multi.7d8b269c.svg"></MenuItem>           
    </div>
</template>

<script>
import MenuItem from './menu/MenuItem.vue'
export default {
    name: "TheHeader",
    props: [],
    components: {
        MenuItem
    },
    data() {
        return {
            
        }
    }
}
</script>

<style scoped>
/* thanh bar */
.div-header {
    height: 64px;
    box-shadow: 0px 1px 3px #ddd;
    display: flex;
    align-items: center;
}

</style>
