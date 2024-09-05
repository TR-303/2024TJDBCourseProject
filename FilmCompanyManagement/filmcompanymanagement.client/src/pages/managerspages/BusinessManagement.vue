<template>
  <div>
    <h2>业务管理</h2>
    <div @click="selectSubComponent('Investment')">投资/成片/租赁</div>
    <div @click="selectSubComponent('ProjectManagement')">项目管理</div>
    <component :is="currentSubComponent" />
  </div>
</template>

<script>
import { ref } from 'vue';
import Investment from './Investment.vue';
import ProjectManagement from './ProjectManagement.vue';

export default {
  name: 'BusinessManagement',
  components: {
    Investment,
    ProjectManagement,
  },
  setup() {
    const currentSubComponent = ref('Investment');

    const selectSubComponent = (component) => {
      currentSubComponent.value = component;
    };

    return {
      currentSubComponent,
      selectSubComponent,
    };
  },
};
</script>

<style>
div {
  cursor: pointer;
  margin-bottom: 10px;
}
</style>
