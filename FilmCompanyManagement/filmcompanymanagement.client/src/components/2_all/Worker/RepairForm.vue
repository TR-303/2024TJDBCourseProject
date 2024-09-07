<template>
    <form @submit.prevent="submitRepair">
        <div class="form-group">
            <label for="repairDate">维修日期</label>
            <input type="date" v-model="form.repairDate" id="repairDate" />
        </div>
        <div class="form-group">
            <label for="equipment">设备编号</label>
            <input type="number" v-model="form.equipment" id="equipment" />
        </div>
        <div class="form-group">
            <label for="cost">维修费用</label>
            <input type="number" v-model="inputAmount" @blur="formatAmount" @input="validateAmount" id="cost" />
        </div>
        <button type="submit" class="submit_button">提交</button>
    </form>
</template>

<script>
    import { ref } from 'vue'
    import axios from 'axios';
    import { useRoute, useRouter } from 'vue-router';

    export default {
        name: 'RepairForm',
        setup() {
            const form = ref({
                repairDate: '',
                equipment: '',
                amount: 0
            })

            const route = useRoute();
            const userID = route.query.id;

            const inputAmount = ref('')

            const validateAmount = (event) => {
                const value = event.target.value
                event.target.value = value.replace(/[^\d.]/g, '')
            }

            const formatAmount = () => {
                const numericValue = parseFloat(inputAmount.value.replace(/,/g, ''))
                if (!isNaN(numericValue)) {
                    form.value.amount = parseFloat(numericValue.toFixed(2))
                    inputAmount.value = form.value.amount.toLocaleString('en-US', {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    })
                } else {
                    form.value.amount = 0
                    inputAmount.value = ''
                }
            }

            const submitRepair = async () => {
                try {
                    // 检查所有字段是否已填写
                    if (!form.value.equipment || !form.value.repairDate || form.value.amount === 0) {
                        alert("请完成所有内容的填写再提交！")
                        return
                    }

                    console.log('begin 1');
                    axios.post('/api/worker/repair', {
                        id: userID,
                        equipmentID: form.value.equipment,
                        date: form.value.repairDate,
                        amount: form.value.amount
                    }).then(function (res) {

                        if (1) {
                            alert("提交成功")
                        } else {
                            alert("提交失败")
                        }
                    })
                } catch (error) {
                    console.error(error);
                    alert("提交失败");
                }
            }

            return { form, inputAmount, validateAmount, formatAmount, submitRepair }
        }
    }
</script>

<style scoped>
    form {
        display: flex;
        flex-direction: column;
        gap: 20px;
        padding: 20px;
        font-size: 18px;
        margin-right: 10%;
    }

    .form-group {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 20px;
    }

    label {
        font-size: 18px;
        margin-right: 20px;
        width: 150px;
    }

    input[type="date"],
    input[type="text"] {
        padding: 10px;
        font-size: 16px;
        border-radius: 5px;
        border: 1px solid #ccc;
        width: calc(100% - 180px);
    }

    .submit_button {
        background-color: white;
        color: #409EFF;
        border: 2px solid #409EFF;
        border-radius: 5px;
        padding: 7px 15px;
        font-size: 18px;
        cursor: pointer;
        text-align: center;
        width: 90px;
        transition: background-color 0.3s, color 0.3s, box-shadow 0.3s;
    }

        .submit_button:hover {
            background-color: #409EFF;
            color: white;
            border: none;
        }
</style>
