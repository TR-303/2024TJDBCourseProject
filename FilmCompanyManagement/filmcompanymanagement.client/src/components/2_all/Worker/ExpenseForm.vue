<template>
    <form @submit.prevent="submitExpense">
        <div class="form-group">
            <label for="travelDate">差旅日期</label>
            <input type="date" v-model="form.travelDate" id="travelDate" />
        </div>
        <div class="form-group">
            <label for="description">支出说明</label>
            <textarea v-model="form.description" id="description" rows="3" cols="60"></textarea>
        </div>
        <div class="form-group">
            <label for="amount">支出金额</label>
            <input type="text" v-model="inputAmount" @blur="formatAmount" @input="validateAmount" id="amount" />
        </div>
        <button type="submit" class="submit_button">提交</button>
    </form>
</template>

<script>
    import { ref } from 'vue'
	import axios from 'axios';

    export default {
        name: 'ExpenseForm',
        setup() {
            const form = ref({
                travelDate: '',
                description: '',
                amount: 0
            })

            const userID = this.$route.query.id;

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

            const submitExpense = async () => {
                // 检查所有字段是否已填写
				if (!form.value.travelDate || !form.value.description || form.value.amount === 0) {
					alert("请完成所有内容的填写再提交！")
					return
				}

                try {
                    console.log('begin 1');
                    axios.post('/api/worker/expense', {
                        id: userID,
                        date: form.value.travelDate,
                        description: form.value.description,
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

            return { form, inputAmount, validateAmount, formatAmount, submitExpense }
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
    textarea,
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
