<template>
  <div class="header">
    <h1>摄影公司管理系统</h1>
    <div class="user-info">
      <span>你好, xiaoming</span>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HeaderComponent'
}
</script>

<style>
.header {
  background-color: #00ccff;
  padding: 10px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.user-info {
  font-size: 16px;
}
</style>
