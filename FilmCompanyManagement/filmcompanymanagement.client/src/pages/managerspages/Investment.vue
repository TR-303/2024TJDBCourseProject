<template>
  <div>
    <h3>投资/成片/租赁</h3>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>日期</th>
          <th>金额</th>
          <th>状态</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(row, index) in rows" :key="index">
          <td>{{ row.id }}</td>
          <td>{{ row.date }}</td>
          <td>{{ row.amount }}</td>
          <td>
            <select v-model="row.status">
              <option value="已完成">已完成</option>
              <option value="待确认">待确认</option>
              <option value="进行中">进行中</option>
              <option value="拒绝">拒绝</option>
            </select>
          </td>
          <td>
            <button @click="removeRow(index)">删除</button>
          </td>
        </tr>
      </tbody>
    </table>
    <button @click="addRow">添加</button>
  </div>
</template>

<script>
import { ref } from 'vue';

export default {
  name: 'Investment',
  setup() {
    const rows = ref([
      { id: 1, date: '2023-07-01', amount: 10000, status: '已完成' },
      { id: 2, date: '2023-07-02', amount: 20000, status: '待确认' },
    ]);

    const addRow = () => {
      rows.value.push({ id: '', date: '', amount: '', status: '待确认' });
    };

    const removeRow = (index) => {
      rows.value.splice(index, 1);
    };

    return {
      rows,
      addRow,
      removeRow,
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
