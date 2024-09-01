<template>
  <div>
    <h2>申请审核</h2>
    <table>
      <thead>
        <tr>
          <th>申请人</th>
          <th>申请类型</th>
          <th>详情</th>
          <th>通过与否</th>
          <th>意见</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(row, index) in rows" :key="index">
          <td>{{ row.applicant }}</td>
          <td>{{ row.type }}</td>
          <td>{{ row.details }}</td>
          <td>
            <select v-model="row.approval">
              <option value="通过">通过</option>
              <option value="不通过">不通过</option>
            </select>
          </td>
          <td>
            <input v-model="row.comments" :disabled="row.approval === '通过'" />
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
  name: 'ApplicationReview',
  setup() {
    const rows = ref([
      { applicant: '张三', type: '请假', details: '病假', approval: '通过', comments: '' },
      { applicant: '李四', type: '加班', details: '项目紧急', approval: '不通过', comments: '原因不足' },
    ]);

    const addRow = () => {
      rows.value.push({ applicant: '', type: '', details: '', approval: '通过', comments: '' });
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
