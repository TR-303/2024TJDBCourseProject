<template>
  <div>
    <h3>项目管理</h3>
    <table>
      <thead>
        <tr>
          <th>员工ID</th>
          <th>姓名</th>
          <th>职位</th>
          <th>状态</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(row, index) in rows" :key="index">
          <td>{{ row.employeeId }}</td>
          <td>{{ row.name }}</td>
          <td>{{ row.position }}</td>
          <td>{{ row.status }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { ref } from 'vue';

export default {
  name: 'ProjectManagement',
  setup() {
    const rows = ref([
      { employeeId: 1, name: '张三', position: '导演', status: '在职' },
      { employeeId: 2, name: '李四', position: '演员', status: '离职' },
    ]);

    return {
      rows,
    };
  },
};
</script>

<style>
table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  border: 1px solid #ddd;
  padding: 8px;
}

th {
  background-color: #f2f2f2;
}
</style>
