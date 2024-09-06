<template>
    <form @submit.prevent="submitRepair">
        <div>
            <label for="repairDate">维修日期</label>
            <input type="date" v-model="form.repairDate" id="repairDate" />
        </div>
        <div>
            <label for="equipment">维修设备</label>
            <input type="text" v-model="form.equipment" id="equipment" />
        </div>
        <div>
            <label for="cost">维修费用</label>
            <input type="text" v-model="inputAmount" @blur="formatAmount" @input="validateAmount" id="cost" />
        </div>
        <button type="submit">提交</button>
    </form>
</template>

<script>
    import { ref } from 'vue'
	import axios from 'axios';

    export default {
        name: 'RepairForm',
        setup() {
            const form = ref({
                repairDate: '',
                equipment: '',
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
    textarea {
        resize: vertical;
        overflow: auto;
    }
</style>
