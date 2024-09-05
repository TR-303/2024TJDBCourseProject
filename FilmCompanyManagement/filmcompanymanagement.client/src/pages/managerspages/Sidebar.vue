<template>
  <div class="sidebar">
    <ul>
      <li @click="toggleSubMenu('personnelManagement')">人员管理</li>
      <ul v-if="activeSubMenu === 'personnelManagement'">
        <li @click="changeView('Recruitment')">招聘/实习</li>
        <li @click="changeView('PersonnelOverview')">人员总览</li>
        <li @click="changeView('Training')">培训</li>
      </ul>
      <li @click="changeView('ApplicationApproval')">申请批准</li>
      <li @click="toggleSubMenu('businessManagement')">业务管理</li>
      <ul v-if="activeSubMenu === 'businessManagement'">
        <li @click="changeView('Investment')">投资/成片/租赁</li>
        <li @click="changeView('ProjectManagement')">项目管理</li>
      </ul>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'Sidebar',
  data() {
    return {
      activeSubMenu: ''
    }
  },
  methods: {
    changeView(view) {
      this.$emit('change-view', view);
    },
    toggleSubMenu(menu) {
      this.activeSubMenu = this.activeSubMenu === menu ? '' : menu;
    }
  }
}
</script>

<style>
.sidebar {
  width: 200px;
  background-color: #f0f0f0;
}
ul {
  list-style: none;
  padding: 0;
}
li {
  padding: 10px;
  cursor: pointer;
}
li:hover {
  background-color: #ddd;
}
</style>
