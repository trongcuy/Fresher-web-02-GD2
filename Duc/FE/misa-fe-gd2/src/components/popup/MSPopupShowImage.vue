<template>
    <div class="overlay">
        <div class="div-image">
            <div class="header">
                <img src="../../assets/img/x.jpg"/>
            </div>
        </div>
    </div>
</template>
  
<script>
export default {
    name: 'MSPopupShowImage',
};
</script>
  
<style scoped lang="scss">
.overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 101;
    .div-image {
        position: absolute;
        transform: translate(-50%, -50%);
        top: 50%;
        left: 50%;
        width: 840px;
        border-radius: 10px;
        border-radius: 10px;
        margin: 0 auto 50px;
        background: #fff;
        box-sizing: border-box;
        .header {
            padding: 20px;
            position: relative;
            img {
                width: 20px;
                height: 20px;
                top: 16px;
                right: 16px;
                position: absolute;
            }
        }
    }
}
</style>
  